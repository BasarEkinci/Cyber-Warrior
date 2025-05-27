using System.Collections.Generic;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class CmpBotVFXPlayer : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> muzzleFlashSystems;

        public void PlayVFX()
        {
            foreach (var particle in muzzleFlashSystems)
            {
                particle?.Play();
            }
        }
     }
}