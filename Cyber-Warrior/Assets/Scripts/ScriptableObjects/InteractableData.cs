using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Scriptable Objects/Interactable Data", order = 0)]
    public class InteractableData : ScriptableObject
    {
        public float interactDuration = 2f;
        public UnityEvent onPress;
        public UnityEvent onHold;
    }
}