using Data.UnityObjects;
using Data.UnityObjects.Events;
using Enums;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private int scarpToAdd = 300;
    [SerializeField] private int scarpToSpend = 100;
 
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameState gameState;
    [SerializeField] private GameStateEvent gameStateEvent;
    
    [Button]
    public void ChangeGameState()
    {
        gameStateEvent.RaiseEvent(gameState);
    }
    [Button]
    public void AddScarp()
    {
        ScarpAmountManager.Instance.AddScarp(scarpToAdd);
    }
    [Button]
    public void SpendScarp()
    {
        ScarpAmountManager.Instance.TrySpendScarp(scarpToSpend);
    }
    
    [Button]
    public void DamagePlayer()
    {
        playerHealth.TakeDamage(damageAmount);
    }
}
