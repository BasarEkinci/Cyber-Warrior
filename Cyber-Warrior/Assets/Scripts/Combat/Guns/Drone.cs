using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Player;
using ScriptableObjects;
using UnityEngine;
using Combat.Components;

namespace Combat.Guns
{
    public class Drone : MonoBehaviour, IRangedGun, IUpgradeable
    {
        #region Serialized Fields

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
            _drone = Resources.Load<RangedGun>("UnityObjects/Guns/Ranged/Drone");
            _collider = GetComponent<BoxCollider>();
            _bulletPool = new ObjectPool();
            _bulletPool.Initialize(_drone.bulletPrefab, _drone.capacity, attackPoint);
            _collider.size = new Vector3(_drone.range, 1, _drone.range);
            _player = GameObject.FindAnyObjectByType<PlayerManager>().transform;
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
            if (other.CompareTag("Enemy"))
            {
                _enemyHolder.EnemyList.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemyHolder.EnemyList.Remove(other.gameObject);
            }
        }

        public void Fire()
        {
            _canFire = _enemyHolder.EnemyList.Count > 0;


            if (!_canFire)
            {
                _fireTiming = 0;
                return;
            }
            _fireTiming += Time.deltaTime;

            if (_fireTiming >= _drone.fireRate)
            {
                var bullet = _bulletPool.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = attackPoint.position;
                    bullet.transform.rotation = attackPoint.rotation;
                    bullet.GetComponent<Bullet>().SetValues(_drone.damage);
                    bullet.GetComponent<Bullet>().AddForce(attackPoint.transform.forward, _drone.bulletForce);
                }
                ReturnBulletToPool(bullet, 0.5f);
                _fireTiming = 0;
            }
        }

        public void LockTarget()
        {
            _enemyHolder.CalculateClosestEnemy(transform.position);
            if (_enemyHolder.EnemyList.Count > 0) head.transform.LookAt(_enemyHolder.peekEnemy.transform);
        }

        public void Upgrade(float amount)
        {
            throw new NotImplementedException();
        }
    }
}