using Interfaces;
using UnityEngine;

namespace Combat.Components
{
    public class Bullet : MonoBehaviour
    {
        private float _damage;
        private float _knockBackForce;
        private int _maxHitCount = 1;
        private Rigidbody _rb;
        private Vector3 _firstSpawnPos;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _firstSpawnPos = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.GetDamage(_damage);
                if (other.TryGetComponent<IKnockbackable>(out var knockBackable))
                {
                    knockBackable.Knockback(-other.transform.forward, _knockBackForce);
                }
                _maxHitCount--;
                if (_maxHitCount == 0)
                {
                    _damage = 0;
                    gameObject.SetActive(false);
                    _rb.linearVelocity = Vector3.zero;
                }
            }
        }

        public void AddForce(Vector3 direction, float force)
        {
            _rb.AddForce(direction * force, ForceMode.VelocityChange);
        }
        /// <summary>
        /// Set the values of the bullet.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="knockBackForce"></param>
        /// <param name="maxHitCount">Represents how many times the bullet can damage</param>
        public void SetValues(float damage, float knockBackForce, int maxHitCount = 1)
        {
            _damage = damage;
            _maxHitCount = maxHitCount;
            _knockBackForce = knockBackForce;
        }
        
        private void OnDisable()
        {
            _rb.linearVelocity = Vector3.zero;
            transform.position = _firstSpawnPos;
        }
        
    }
}
