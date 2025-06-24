using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioDatabaseSo audioDatabaseSo;
        private AudioSource _audioSource;
        public void SetClip(SfxType sfxType,AudioSource source,bool isLooping = false)
        {
            if (source != null) source.clip = audioDatabaseSo.Audios[sfxType].Clip;
        }
        
        public void PlaySfx(SfxType sfxType,AudioSource source)
        {
            source.PlayOneShot(
                audioDatabaseSo.Audios[sfxType].Clip,
                audioDatabaseSo.Audios[sfxType].Volume
            );
        }
    }
}
