using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private ScrapData scrapData;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text scrapAmountText;
        
        private void OnEnable()
        {
            scrapData.OnScrapAmountChanged += UpdateScrapText;
            UpdateScrapText();
        }

        private void UpdateScrapText()
        {
            scrapAmountText.text = ": " + scrapData.currentScarp;
        }

        private void OnDisable()
        {
            scrapData.OnScrapAmountChanged -= UpdateScrapText;
        }
    }
}
