using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        
        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarBackground;
        [SerializeField] private HealthEvent healthEvent;
        private Camera _mainCamera;

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
            healthEvent.OnHealthChanged += UpdateHealthUI;
        }
        private void Update()
        {
            transform.LookAt(_mainCamera.transform);
        }
        private void OnDisable()
        {
            healthEvent.OnHealthChanged -= UpdateHealthUI;
        }
        private void UpdateHealthUI(float newHealth)
        {
            healthBar.fillAmount = newHealth / 100;
        }
    }
}
