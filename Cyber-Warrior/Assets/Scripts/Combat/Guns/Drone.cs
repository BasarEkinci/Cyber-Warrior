using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Player;
using ScriptableObjects;
using UnityEngine;
using Combat.Components;
using Enemies;
using ScriptableObjects.Events;

namespace Combat.Guns
{
    public class Drone : MonoBehaviour, IRangedGun, IUpgradeable
    {
        #region Serialized Fields
        [SerializeField] private EnemyDeathEvent enemyDeathEvent;
        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        [Header("Components")]
        [SerializeField] private GameObject head;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Vector3 followOffset;
        #endregion
        #region Private Fields

        private RangedGun _drone;
        private EnemyHolder _enemyHolder;
        private BoxCollider _collider;
        private ObjectPool _bulletPool;
        private bool _canFire;
        private float _fireTiming;
        private Transform _player;

        #endregion
        private void OnEnable()
        {
            _enemyHolder = new EnemyHolder();
            enemyDeathEvent.OnEnemyDeath += OnEnemyDeath;
            playerDeathEvent.OnPlayerDeath += OnPlayerDeath;
            _drone = Resources.Load<RangedGun>("UnityObjects/Guns/Ranged/Drone");
            _collider = GetComponent<BoxCollider>();
            _bulletPool = new ObjectPool();
            _bulletPool.Initialize(_drone.bulletPrefab, _drone.capacity, attackPoint);
            _collider.size = new Vector3(_drone.range, 1, _drone.range);
            _player = FindAnyObjectByType<PlayerManager>().transform;
            _canFire = true;
        }

        private void OnPlayerDeath()
        {
            _canFire = false;
        }

        private void OnEnemyDeath(GameObject enemy)
        {
            _enemyHolder.EnemyList.Remove(enemy);
        }

        private void FixedUpdate()
        {
            if (_player != null)
            {
                transform.position = Vector3.Lerp(transform.position, _player.position + followOffset, Time.deltaTime * 5f);
            }
        }

        private void Update()
        {
            Fire();
            LockTarget();

        }
        private async void ReturnBulletToPool(GameObject bullet, float extraTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_drone.fireRate + extraTime));
            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            _bulletPool.ReturnObject(bullet, attackPoint);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (enemy.CurrentHealth > 0f)
                    _enemyHolder.EnemyList.Add(other.gameObject);
            }
            _enemyHolder.CalculateClosestEnemy(transform.position);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemyHolder.EnemyList.Remove(other.gameObject);
            }
            _enemyHolder.CalculateClosestEnemy(transform.position);
        }

        public void Fire()
        {
            if (_enemyHolder.EnemyList.Count <= 0 || !_canFire) return;

            _fireTiming += Time.deltaTime;

            if (_fireTiming >= _drone.fireRate)
            {
                var bullet = _bulletPool.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = attackPoint.position;
                    bullet.transform.rotation = attackPoint.rotation;
                    bullet.GetComponent<Bullet>().SetValues(_drone.damage, _drone.knockBackForce);
                    bullet.GetComponent<Bullet>().AddForce(attackPoint.transform.forward, _drone.bulletForce);
                }
                ReturnBulletToPool(bullet, 0.5f);
                _fireTiming = 0;
            }
        }

        public void LockTarget()
        {
            _enemyHolder.CalculateClosestEnemy(transform.position);
            if (_enemyHolder.EnemyList.Count > 0)
            {
                head.transform.LookAt(_enemyHolder.peekEnemy.transform.position + Vector3.up * 1.5f);
            }
        }

        public void Upgrade(float amount)
        {
            throw new NotImplementedException();
        }
    }
}