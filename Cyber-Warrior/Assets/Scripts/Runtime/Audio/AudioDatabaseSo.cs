using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Scriptable Objects/AudioDatabase", order = 0)]
    public class AudioDatabaseSo : SerializedScriptableObject
    {
        public Dictionary<SfxType, AudioData> Audios;
    }
}