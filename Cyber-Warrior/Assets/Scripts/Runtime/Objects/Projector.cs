using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Objects
{
    public class Projector : MonoBehaviour
    {
        [SerializeField] private Light projectorLight;
        [SerializeField] private GameObject projectorLine;

        private void OnEnable()
        {
            projectorLight.enabled = false;
            projectorLine.transform.localScale = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                projectorLight.enabled = true;
                projectorLine.transform.DOScale(Vector3.one / 100f, 0.1f);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                projectorLight.enabled = false;
                projectorLine.transform.DOScale(Vector3.zero, 0.1f);
            }
        }
    }
}
