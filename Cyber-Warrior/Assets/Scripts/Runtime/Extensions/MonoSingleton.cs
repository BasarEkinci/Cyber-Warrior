using System;
using UnityEngine;

namespace Runtime.Extensions
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var existingInstance = FindObjectOfType<T>();
            if (existingInstance != null)
                return existingInstance;
        
            GameObject singletonObject = new GameObject(typeof(T).Name);
            return singletonObject.AddComponent<T>();
        });

        public static T Instance => _instance.Value;

        protected virtual void Awake()
        {
            if (_instance.IsValueCreated && _instance.Value != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}