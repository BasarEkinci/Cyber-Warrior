using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioDatabaseSo audioDatabaseSo;
        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetClip(SfxType sfxType,AudioSource source,bool isLooping = false)
        {
            if (source != null) source.clip = audioDatabaseSo.Audios[sfxType].Clip;
        }
        
        public void PlaySfx(SfxType sfxType,AudioSource source = null)
        {
            if (source == null)
            {
                source = _audioSource;
            }
            source.PlayOneShot(
                audioDatabaseSo.Audios[sfxType].Clip,
                audioDatabaseSo.Audios[sfxType].Volume
            );
        }
    }
}
