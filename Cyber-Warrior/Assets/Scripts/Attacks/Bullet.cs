using UnityEngine;

namespace Attacks.Objects
{
    public class Bullet : MonoBehaviour
    {

        private float _damage;
        private int _maxHitCount = 1;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _maxHitCount--;
                // other.gameObject.GetComponent<EnemyHealth>().TakeDamage(_damage);   
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

        public void SetValues(float damage, int maxHitCount = 1)
        {
            _damage = damage;
            _maxHitCount = maxHitCount;
        }
    }
}
