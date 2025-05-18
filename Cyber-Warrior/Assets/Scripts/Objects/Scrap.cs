using DG.Tweening;
using ScriptableObjects.Events;
using UnityEngine;

namespace Objects
{
    public class Scrap : MonoBehaviour
    {
        [SerializeField] private VoidEventSO scrapCollectEvent;
        
        private Tween _tween;

        private void OnEnable()
        {
            _tween = transform.DORotate(Vector3.up * 45,0.5f).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);    
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                scrapCollectEvent.Invoke();
                CollectAnimation(other.transform);
            }
        }

        private void CollectAnimation(Transform playerTransform)
        {
            transform.DOMove(playerTransform.position, 0.1f).SetEase(Ease.OutQuart).OnComplete(() =>
            {
                //SFX
                //VFX
                //Destroy
            });
        }

        private void OnDisable()
        {
            if (_tween.IsActive())
            {
                _tween.Kill();
            }
        }
    }
}
