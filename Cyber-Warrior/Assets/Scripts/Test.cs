using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [Header("Test Values")]
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private int scarpToAdd = 300;
    [SerializeField] private int scarpToSpend = 100;
    [Header("Class References")]
    [SerializeField] private PlayerHealth playerHealth;
    [Header("Data References")]
    [SerializeField] private GameState gameState;
    [SerializeField] private GameStateEvent gameStateEvent;


    private void Start()
    {
        gameStateEvent.RaiseEvent(GameState.Base);
    }

    #region Test Methods
    [Title("Changes the game state with specified value")]
    [Button(ButtonSizes.Large)]
    public void ChangeGameState()
    {
        gameStateEvent.RaiseEvent(gameState);
    }
    [PropertySpace]
    [Title("Adds the Scarp up to given value")]
    [Button(ButtonSizes.Large)]
    public void AddScarp()
    {
        ScarpAmountManager.Instance.AddScarp(scarpToAdd);
    }

    [PropertySpace]
    [Title("Spends the Scarp up to given value")]
    [Button(ButtonSizes.Large)]
    public void SpendScarp()
    {
        ScarpAmountManager.Instance.TrySpendScarp(scarpToSpend);
    }
    
    [PropertySpace]
    [Title("Damages the player up to given value")]
    [Button(ButtonSizes.Large)]
    public void DamagePlayer()
    {
        playerHealth.TakeDamage(damageAmount);
    }

    [PropertySpace]
    [Title("Reload the Scene")]
    [Button(ButtonSizes.Large)]
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}