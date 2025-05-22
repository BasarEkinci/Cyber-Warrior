using Data.UnityObjects;
using Data.UnityObjects.Events;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameStateEvent gameStateEvent;
    
    [Button]
    public void ExecuteEvent()
    {
        gameStateEvent.RaiseEvent(gameState);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ScarpAmountManager.Instance.AddScarp(300);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ScarpAmountManager.Instance.TrySpendScarp(120);
        }
    }
}
