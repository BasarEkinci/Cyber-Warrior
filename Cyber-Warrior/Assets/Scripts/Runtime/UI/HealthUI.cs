using Data.UnityObjects.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class HealthUI : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color healthColor;
        [SerializeField] private Color damageColor;
        
        [Header("References")]
        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarBackground;
        [SerializeField] private FloatEventChannel healthEventSo;
        
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
            healthEventSo.OnEventRaised += UpdateHealthUI;
        }
        
        private void LateUpdate()
        {
            transform.LookAt(_mainCamera.transform);
        }
        private void OnDisable()
        {
            healthEventSo.OnEventRaised -= UpdateHealthUI;
        }
        private void UpdateHealthUI(float newHealth)
        {
            if (_previousHealth > newHealth)
            {
                healthBar.DOColor(Color.red,0.1f).OnComplete(()=> healthBar.DOColor(defaultColor, 0.1f));
            }
            else
            {
                healthBar.DOColor(healthColor, 0.1f).OnComplete(() => healthBar.DOColor(defaultColor,0.1f));
            }
            
            healthBar.fillAmount = newHealth / 100;
            _previousHealth = newHealth;
        }
    }
}
