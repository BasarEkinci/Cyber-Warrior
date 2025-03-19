using System.Collections;
using Extensions;
using ScriptableObjects;
using UnityEngine;

namespace Attacks.Guns
{
    public class Drone : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")]
        [SerializeField] private GameObject head;
        [SerializeField] private Transform attackPoint;
        #endregion
        #region Private Fields

        private RangedGun _drone;
        private EnemyHolder _enemyHolder;
        private BoxCollider _collider;
        private ObjectPool _bulletPool;
        private bool _canFire;
        private float _fireTiming;

        #endregion
        private void OnEnable()
        {
            _enemyHolder = new EnemyHolder();
            _drone = Resources.Load<RangedGun>("UnityObjects/Guns/Ranged/Drone");
            _collider = GetComponent<BoxCollider>();
            _bulletPool = new ObjectPool();
            _bulletPool.Initialize(_drone.bulletPrefab, _drone.capacity, attackPoint);
            _collider.size = new Vector3(_drone.range, 1, _drone.range);
        }
        private void Update()
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

                StartCoroutine(Attack(bullet, 1f));
                _fireTiming = 0;
            }
            _enemyHolder.CalculateClosestEnemy(transform.position);
            if (_enemyHolder.EnemyList.Count > 0) head.transform.LookAt(_enemyHolder.peekEnemy.transform);
        }



        private IEnumerator Attack(GameObject bullet, float extraTime)
        {
            yield return new WaitForSeconds(_drone.fireRate + extraTime);
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
            if (other.CompareTag("Enemy")) _enemyHolder.EnemyList.Remove(other.gameObject);
        }
    }
}