using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public class ObjectPool
    {
        private Queue<GameObject> _pool = new();

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

        public GameObject GetObject()
        {
            if (_pool.Count == 0)
            {
                return null;
            }
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}