using DG.Tweening;
using Runtime.Audio;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Objects
{
    public class Scrap : MonoBehaviour
    {
        [SerializeField] private GameObject scrapVFX;
        private Tween _tween;
        private GameObject _vfx;
        private AudioSource _audioSource;
        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            transform.DOJump(transform.position + Vector3.up,1f,2,2f).SetEase(Ease.OutBounce);
            _tween = transform.DORotate(transform.up * 45f,0.5f).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
            _vfx = Instantiate(scrapVFX,transform.position,Quaternion.identity * Quaternion.Euler(-90,0,0));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ScarpAmountManager.Instance.AddScarp(1);
                AudioManager.Instance.PlaySfx(SfxType.PickupScrap, _audioSource);
                Destroy(_vfx);
                CollectAnimation(other.transform);
            }
        }

        private void CollectAnimation(Transform playerTransform)
        {
            _tween.Kill();
            transform.DOMove(playerTransform.position, 0.1f).SetEase(Ease.OutQuart);
            Destroy(gameObject,0.11f);
        }
    }
}
