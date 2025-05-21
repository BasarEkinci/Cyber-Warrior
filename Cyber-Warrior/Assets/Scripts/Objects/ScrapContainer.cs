using Data.UnityObjects;
using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class ScrapContainer : MonoBehaviour
    {
        [SerializeField] private float scarpHeightBound;
        [SerializeField] private GameObject scrapStack;
        private int _previousScrapAmount;
        private void OnEnable()
        {
            _previousScrapAmount = ScarpAmountManager.Instance.currentScarp;   
            ScarpAmountManager.Instance.onScrapAmountChanged += UpdateStackLevel;
        }
        
        private void UpdateStackLevel(int amount)
        {
            if (_previousScrapAmount > amount)
                scrapStack.transform.DOMove(scrapStack.transform.position + Vector3.down * (amount / 10f), 0.1f).SetEase(Ease.OutBounce);
            else
                scrapStack.transform.DOMove(scrapStack.transform.position + Vector3.up * (amount / 10f), 0.1f).SetEase(Ease.OutBounce);
            _previousScrapAmount = ScarpAmountManager.Instance.currentScarp;
        }
        
        private void OnDisable()
        {
            ScarpAmountManager.Instance.onScrapAmountChanged -= UpdateStackLevel;
        }
    }
}
