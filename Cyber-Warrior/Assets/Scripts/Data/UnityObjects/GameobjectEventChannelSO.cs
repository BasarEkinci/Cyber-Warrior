using UnityEngine;
using UnityEngine.Events;

namespace Data.UnityObjects
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
