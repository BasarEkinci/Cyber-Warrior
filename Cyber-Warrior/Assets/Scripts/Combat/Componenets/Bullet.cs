using Combat.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.Components
{
    public class Bullet : MonoBehaviour
    {

        private float _damage;
        private float _knockBackForce;
        private int _maxHitCount = 1;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.GetDamage(_damage);
                if (other.TryGetComponent<IKnockbackable>(out IKnockbackable _knockBackable))
                {
                    _knockBackable.Knockback(-other.transform.forward, _knockBackForce);
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

        public void SetValues(float damage, float knockBackForce, int maxHitCount = 1)
        {
            _damage = damage;
            _maxHitCount = maxHitCount;
            _knockBackForce = knockBackForce;
        }
    }
}
