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
            return null;
        }
        
        public void ReturnEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                return;
            }
            enemy.gameObject.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }
}