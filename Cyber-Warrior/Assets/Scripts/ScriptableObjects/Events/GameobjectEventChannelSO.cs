using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = "Scriptable Objects/Events/GameObjectEvent")]
    public class GameobjectEventChannelSO : ScriptableObject
    {
        public UnityAction<GameObject> OnEventRaised;

        public void Invoke(GameObject obj)
        {
            OnEventRaised?.Invoke(obj);
        }
    }
}
