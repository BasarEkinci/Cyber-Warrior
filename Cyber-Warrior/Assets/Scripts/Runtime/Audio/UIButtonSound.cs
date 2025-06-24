using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Audio
{
    public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioManager.Instance.PlaySfx(SfxType.ButtonHighlight,_audioSource);
        }
    }
}
