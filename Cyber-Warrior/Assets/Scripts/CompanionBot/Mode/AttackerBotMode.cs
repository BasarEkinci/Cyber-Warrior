using Interfaces;
using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class AttackerBotMode : ICmpBotModeStrategy,IAttackerCmp
    {
        private GameObject _target;
        private Transform _companionTransform;
        private Transform _currentTarget;
        private LayerMask _enemyLayer;

        private float _searchCooldown = 0.5f;
        private float _lastSearchTime = -Mathf.Infinity;
        private float _searchRadius = 20f;
        
        public void Execute(Rotator rotator,GameObject referance, float rotationSpeed)
        {
            Transform target = GetEnemyTarget();
            if (target != null)
            {
                rotator.RotateToTarget(target.gameObject, rotationSpeed);
            }
            else
            {
                rotator.RotateToTarget(referance, rotationSpeed);
            }
        }
        
        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.red;
        }
        public void SetAttackerProperties(Transform companionTransform, LayerMask layerMask)
        {
            _companionTransform = companionTransform;
            _enemyLayer = layerMask;
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

        private void Attack()
        {
            
        }
    }
}