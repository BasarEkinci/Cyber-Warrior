using System.Collections;
using System.Collections.Generic;
using Attacks.Objects;
using Extensions;
using UnityEngine;

namespace Attacks
{
    public class Drone : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private GameObject head;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private GameObject bulletPrefab;

        [Header("Attack Properties")] [SerializeField]
        private float damage;

        [SerializeField] private float range;
        [SerializeField] private float attackRate;
        [SerializeField] private float bulletForce;
        [SerializeField] private int maxBulletCount;

        #endregion

        #region Private Fields

        private BoxCollider _collider;
        private GameObject _peekedEnemy;
        private List<GameObject> _enemiesInRange;
        private ObjectPool _bulletPool;
        private bool _canFire;
        private float _fireTiming;

        #endregion

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _enemiesInRange = new List<GameObject>();
            _bulletPool = new ObjectPool();
        }

        private void Start()
        {
            _collider.size = new Vector3(range, 1, range);
        }

        private void OnEnable()
        {
            _bulletPool.Initialize(bulletPrefab, maxBulletCount);
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

            if (_fireTiming >= attackRate)
            {
                var bullet = _bulletPool.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = attackPoint.position;
                    bullet.transform.rotation = attackPoint.rotation;
                    bullet.GetComponent<DroneBullet>().SetValues(damage);
                    bullet.GetComponent<DroneBullet>().AddForce(attackPoint.transform.forward, bulletForce);
                }

                StartCoroutine(Attack(bullet));
                _fireTiming = 0;
            }

            if (_enemiesInRange.Count > 1) CalculateClosestEnemy();
            if (_peekedEnemy != null) head.transform.LookAt(_peekedEnemy.transform);
            if (_enemiesInRange.Count == 0) _peekedEnemy = null;
        }

        private IEnumerator Attack(GameObject bullet)
        {
            yield return new WaitForSeconds(attackRate);
            _bulletPool.ReturnObject(bullet);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (_enemiesInRange.Count == 0)
                {
                    Debug.Log("Enemy Entered");
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