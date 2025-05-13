using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {

        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarBackground;
        [FormerlySerializedAs("healthEvent")] [SerializeField] private HealthEventChannelSO healthEventChannelSo;
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
            healthEventChannelSo.OnHealthChanged += UpdateHealthUI;
        }
        private void LateUpdate()
        {
            transform.LookAt(_mainCamera.transform);
        }
        private void OnDisable()
        {
            healthEventChannelSo.OnHealthChanged -= UpdateHealthUI;
        }
        private void UpdateHealthUI(float newHealth)
        {
            healthBar.fillAmount = newHealth / 100;
        }
    }
}
