using System;
using UnityEngine;

namespace Runtime.Audio
{
    [Serializable]
    public struct AudioData
    {
        public AudioClip Clip;
        public float Volume;
    }
}