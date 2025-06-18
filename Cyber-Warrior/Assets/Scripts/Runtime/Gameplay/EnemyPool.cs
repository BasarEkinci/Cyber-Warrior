using System.Collections.Generic;
using Runtime.Enemies;
using UnityEngine;

namespace Runtime.Gameplay
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private int poolSize = 100;
        
        private readonly Queue<Enemy> _enemyPool = new();

        private void Awake()
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not assigned in the EnemyPool.");
                return;
            }

            for (int i = 0; i < poolSize; i++)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform);
                enemy.gameObject.SetActive(false);
                _enemyPool.Enqueue(enemy);
            }
        }
        
        public Enemy GetEnemy(Vector3 spawnPosition)
        {
            if (_enemyPool.Count > 0)
            {
                Enemy enemy = _enemyPool.Dequeue();
                enemy.transform.position = spawnPosition;
                enemy.gameObject.SetActive(true);
                return enemy;
            }
            else
            {
                Debug.LogWarning("No enemies available in the pool. Consider increasing the pool size.");
                return null;
            }
        }
        
        public void ReturnEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                Debug.LogError("Attempted to return a null enemy to the pool.");
                return;
            }

            enemy.gameObject.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }
}