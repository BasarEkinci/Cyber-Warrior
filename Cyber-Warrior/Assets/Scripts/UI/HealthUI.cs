using Data.UnityObjects;
using Data.UnityObjects.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarBackground;
        [SerializeField] private FloatEventChannel healthEventSO;
        
        private Camera _mainCamera;
        private float _previousHealth;
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        private void Start()
        {
            healthBar.fillAmount = 1;
        }
        private void OnEnable()
        {
            healthEventSO.OnEventRaised += UpdateHealthUI;
        }
        private void LateUpdate()
        {
            transform.LookAt(_mainCamera.transform);
        }
        private void OnDisable()
        {
            healthEventSO.OnEventRaised -= UpdateHealthUI;
        }
        private void UpdateHealthUI(float newHealth)
        {
            if (_previousHealth > newHealth)
            {
                healthBar.DOColor(Color.red,0.1f).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                healthBar.DOColor(Color.green,0.1f).SetLoops(2, LoopType.Yoyo);
            }
            
            healthBar.fillAmount = newHealth / 100;
            _previousHealth = newHealth;
        }
    }
}
