using System.Collections;
using System.Collections.Generic;
using Attacks.Objects;
using Extensions;
using ScriptableObjects;
using UnityEngine;

namespace Attacks
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
        private BoxCollider _collider;
        private GameObject _peekedEnemy;
        private List<GameObject> _enemiesInRange;
        private ObjectPool _bulletPool;
        private bool _canFire;
        private float _fireTiming;

        #endregion
        private void OnEnable()
        {
            _drone = Resources.Load<RangedGun>("UnityObjects/Guns/Ranged/Drone");
            _collider = GetComponent<BoxCollider>();
            _enemiesInRange = new List<GameObject>();
            _bulletPool = new ObjectPool();

            _bulletPool.Initialize(_drone.bulletPrefab, _drone.capacity, attackPoint);
            _collider.size = new Vector3(_drone.range, 1, _drone.range);
        }
        private void Update()
        {
            _canFire = _enemiesInRange.Count > 0;

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
                    bullet.GetComponent<DroneBullet>().SetValues(_drone.damage);
                    bullet.GetComponent<DroneBullet>().AddForce(attackPoint.transform.forward, _drone.bulletForce);
                }

                StartCoroutine(Attack(bullet, 0.2f));
                _fireTiming = 0;
            }

            if (_enemiesInRange.Count > 1) CalculateClosestEnemy();
            if (_peekedEnemy != null) head.transform.LookAt(_peekedEnemy.transform);
            if (_enemiesInRange.Count == 0) _peekedEnemy = null;
        }



        private IEnumerator Attack(GameObject bullet, float extraTime)
        {
            yield return new WaitForSeconds(_drone.fireRate + extraTime);
            _bulletPool.ReturnObject(bullet);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (_enemiesInRange.Count == 0)
                {
                    _peekedEnemy = other.gameObject;
                }

                _enemiesInRange.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy")) _enemiesInRange.Remove(other.gameObject);
        }


        private void CalculateClosestEnemy()
        {
            for (var i = 0; i < _enemiesInRange.Count; i++)
                if (_enemiesInRange.Count == 1)
                {
                    _peekedEnemy = _enemiesInRange[i];
                }
                else
                {
                    if (Vector3.Distance(_peekedEnemy.transform.position, transform.position) >
                        Vector3.Distance(_enemiesInRange[i].transform.position, transform.position))
                        _peekedEnemy = _enemiesInRange[i];
                }
        }
    }
}