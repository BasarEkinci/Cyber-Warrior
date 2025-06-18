using System.Collections;
using System.Collections.Generic;
using Runtime.Enemies;
using UnityEngine;

namespace Runtime.Gameplay
{
    public class EnemyTickManager : MonoBehaviour
    {
        [SerializeField] private float tickInterval = 0.2f;
        private readonly List<Enemy> _enemies = new List<Enemy>();

        private void Start() => StartCoroutine(TickLoop());

        IEnumerator TickLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickInterval);
                foreach (var ai in _enemies)
                    ai.Tick(); 
            }
        }

        public void Register(Enemy ai) => _enemies.Add(ai);
        public void Unregister(Enemy ai) => _enemies.Remove(ai);
    }
}
