using Enemies;
using Interfaces;
using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class AttackerBotMode : ICmpBotModeStrategy,IAttackerCmp,IAttackEffect
    {
        private float _attackCooldown;
        private float _timer;
        private float _damage;
        private GameObject _target;
        private Transform _companionTransform;
        private Transform _currentTarget;
        private ParticleSystem _attackEffect;
        private LayerMask _enemyLayer;

        private readonly float _searchCooldown = 0.5f;
        private readonly float _searchRadius = 20f;
        private float _lastSearchTime = -Mathf.Infinity;
        
        public void Execute(Rotator rotator,GameObject reference, float rotationSpeed)
        {
            Transform target = GetEnemyTarget();
            if (target != null)
            {
                rotator.RotateToTarget(target.gameObject, rotationSpeed,Vector3.up * 1.5f);
                Attack(target);
            }
            else
            {
                rotator.RotateToTarget(reference, rotationSpeed);
            }
        }
        
        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.red;
        }
        public void SetAttackerProperties(Transform companionTransform, LayerMask layerMask,float damage,float attackCooldown = 1f)
        {
            _attackCooldown = attackCooldown;
            _companionTransform = companionTransform;
            _enemyLayer = layerMask;
            _damage = damage;
            _timer = _attackCooldown;
        }

        public Transform FindClosestEnemy()
        {
            Collider[] hits = Physics.OverlapSphere(_companionTransform.position, _searchRadius, _enemyLayer);

            Transform closestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            foreach (var hit in hits)
            {
                float distance = Vector3.Distance(_companionTransform.position, hit.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    if (hit.transform.GetComponent<Enemy>().IsDead)
                    {
                        return null;
                    }
                    closestEnemy = hit.transform;
                }
            }

            return closestEnemy;
        }

        public Transform GetEnemyTarget()
        {
            if (Time.time >= _lastSearchTime + _searchCooldown)
            {
                _currentTarget = FindClosestEnemy();
                _lastSearchTime = Time.time;
            }
            return _currentTarget;
        }

        public void Attack(Transform target)
        {
            if (target == null) return;

            if (Time.time >= _attackCooldown)
            {
                target.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
                PlayAttackEffect();
                _attackCooldown = Time.time + _timer;
            }
        }

        public void SetAttackEffect(ParticleSystem effect)
        {
            _attackEffect = effect;
        }

        public void PlayAttackEffect()
        {
            if (_attackEffect == null) return;
            _attackEffect.Play();
        }
        public void ModeBehaviour()
        {
        }
    }
}