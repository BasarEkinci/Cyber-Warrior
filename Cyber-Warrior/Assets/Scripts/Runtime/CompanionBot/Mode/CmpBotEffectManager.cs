using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class CmpBotEffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject eyesLights;
        [SerializeField] private List<GameObject> eyesLines;
        [SerializeField] private List<ParticleSystem> muzzleFlashSystems;

        private void OnEnable()
        {
            CloseEyesLights();
        }

        public void PlayFireEffect()
        {
            foreach (var particle in muzzleFlashSystems)
            {
                particle?.Play();
            }
        }
        public void OpenEyesLights()
        {
            eyesLights.SetActive(true);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0.01f, 0.3f);
            }
        }

        public void CloseEyesLights()
        {
            eyesLights.SetActive(false);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0f, 0.3f);
            }
        }
     }
}