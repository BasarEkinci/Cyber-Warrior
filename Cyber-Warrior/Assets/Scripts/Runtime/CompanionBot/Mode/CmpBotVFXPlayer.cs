using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class CmpBotVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject eyesLights;
        [SerializeField] private List<GameObject> eyesLines;
        [SerializeField] private List<ParticleSystem> muzzleFlashSystems;

        private void OnEnable()
        {
            CloseLights();
        }

        public void PlayFireVFX()
        {
            foreach (var particle in muzzleFlashSystems)
            {
                particle?.Play();
            }
        }
        public void OpenLights()
        {
            eyesLights.SetActive(true);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0.01f, 0.1f);
            }
        }

        public void CloseLights()
        {
            eyesLights.SetActive(false);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0f, 0.1f);
            }
        }
     }
}