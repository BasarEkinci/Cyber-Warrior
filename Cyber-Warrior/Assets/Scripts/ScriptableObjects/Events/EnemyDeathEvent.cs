using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{

}

[CreateAssetMenu(fileName = "EnemyDeathEvent", menuName = "Scriptable Objects/Events/EnemyDeathEvent")]
public class EnemyDeathEvent : ScriptableObject
{
    public UnityAction<GameObject> OnEnemyDeath;

    public void Invoke(GameObject enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }
}
