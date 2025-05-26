using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public class ObjectPool
    {
        private Queue<GameObject> _pool = new();
        private List<GameObject> _activeObjectsList = new();
        public void Initialize(GameObject prefab, int poolSize, Transform parent = null)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Object.Instantiate(prefab);
                if (parent != null)
                    obj.transform.SetParent(parent);
                obj.SetActive(false);
                _pool.Enqueue(obj);
            }
        }
        public void ClearPool()
        {
            foreach (GameObject obj in _activeObjectsList)
            {
                obj.SetActive(false);
                _pool.Enqueue(obj);
            }
            _activeObjectsList.Clear();
        }
        public GameObject GetObject()
        {
            if (_pool.Count == 0)
            {
                return null;
            }
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            _activeObjectsList.Add(obj);
            return obj;
        }
        /// <summary>
        /// Return the object to the pool and set it inactive.
        /// </summary>
        /// <param name="obj">Current active object</param>
        /// <param name="spawnPos">First activate position of the object</param>
        public void ReturnObject(GameObject obj, Transform spawnPos)
        {
            obj.SetActive(false);
            obj.transform.position = spawnPos.position;
            _pool.Enqueue(obj);
        }
    }
}