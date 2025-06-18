using System;
using System.Collections.Generic;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enemies;
using Runtime.Enums;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Runtime.Gameplay
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private GameStateEvent gameStateEvent;
        [Header("Spawn Settings")] 
        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private int maxActiveEnemies = 100;
        [SerializeField] private float spawnRadius = 10f;
        [SerializeField] private float spawnHeight = 1f;

        private float _timer;
        private List<Enemy> _activeEnemies = new();
        private GameState _gameState;
        private bool _canSpawnEnemies;
        private void OnEnable()
        {
            gameStateEvent.OnEventRaised += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            _gameState = state;
            _canSpawnEnemies = _gameState == GameState.Action;
        }

        private void Update()
        {
            if (!_canSpawnEnemies)
                return;
            
            _timer += Time.deltaTime;
            
            if (_timer >= spawnInterval && _activeEnemies.Count < maxActiveEnemies)
            {
                SpawnEnemy();
                _timer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            Enemy enemy = enemyPool.GetEnemy(spawnPosition);
            enemy.InitializeValues(playerTransform);
            if (enemy != null)
            {
                _activeEnemies.Add(enemy);
                enemy.EnemyDeathEvent.OnEventRaised += () => _activeEnemies.Remove(enemy);
            }
            else
            {
                Debug.LogWarning("Failed to spawn enemy: No available enemies in the pool.");
            }
        }

        private Vector3 GetRandomPositionAroundPlayer()
        {
            Vector2 randCircle = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = playerTransform.position + new Vector3(randCircle.x, spawnHeight, randCircle.y);
            return spawnPosition;
        }
    }
}
