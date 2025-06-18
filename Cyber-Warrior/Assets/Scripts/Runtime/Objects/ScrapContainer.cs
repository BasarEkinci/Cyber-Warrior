using Data.UnityObjects;
using DG.Tweening;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Objects
{
    public class ScrapContainer : MonoBehaviour
    {
        [SerializeField] private Transform scrapStack;    
        [SerializeField] private float minStackYPos;      
        [SerializeField] private float maxStackYPos;      
        [SerializeField] private float tweenDuration = 0.3f;
        private Tween _tween;
        
        private void OnEnable()
        {
            ScarpAmountManager.Instance.OnScarpSpend.OnEventRaised += OnScrapSpend;
            ScarpAmountManager.Instance.OnScarpEarned.OnEventRaised += OnScrapEarned;
        }
        private void OnDisable()
        {
            ScarpAmountManager.Instance.OnScarpSpend.OnEventRaised -= OnScrapSpend;
            ScarpAmountManager.Instance.OnScarpEarned.OnEventRaised -= OnScrapEarned;
        }
        private void OnScrapEarned(int amount)
        {
            if (scrapStack.localPosition.y >= maxStackYPos)
            {
                return;
            }
            _tween?.Kill();
            Vector3 targetPos = scrapStack.transform.position + Vector3.up * 0.1f;
            _tween = scrapStack.DOMove(targetPos, tweenDuration).SetEase(Ease.OutBounce);
        }
        private void OnScrapSpend(int amount)
        {
            if (scrapStack.localPosition.y <= minStackYPos)
            {
                return;
            }
            _tween?.Kill();
            Vector3 targetPos = scrapStack.transform.position + Vector3.down * 0.1f;
            _tween = scrapStack.DOMove(targetPos, tweenDuration).SetEase(Ease.OutBounce);
        }
    }
}