using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class Converter : MonoBehaviour, IAnimated
    {
        [SerializeField] private ParticleSystem smoke;
        public void Animate()
        {
            transform.DOShakePosition(0.5f,1f,10,90);
            smoke.Play();
        }
    }
}
