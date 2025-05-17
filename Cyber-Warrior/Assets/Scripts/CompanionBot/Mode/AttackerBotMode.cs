using Enemies;
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

        private readonly float _searchCooldown = 0.5f;
        private readonly float _searchRadius = 20f;
        private float _lastSearchTime = -Mathf.Infinity;
        
        public void Execute(Rotator rotator,GameObject reference, float rotationSpeed)
        {
            Transform target = GetEnemyTarget();
            if (target != null)
            {
                rotator.RotateToTarget(target.gameObject, rotationSpeed);
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

        public void Attack(Transform bulletSpawnPoint, Transform target)
        {
            //Attack Logic Here
        }
    }
}