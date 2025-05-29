using Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "StatChange", menuName = "Scriptable Objects/Events/StatChangeEvent")]
    public class StatChangeEvent : EventChannelSO<StatType> 
    {
        
    }
}