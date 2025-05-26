using System;
using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;

        private void OnEnable()
        {
            infoPanel.transform.DOScale(Vector3.zero,0.1f).OnComplete(() =>
            {
                infoPanel.SetActive(false);
            });
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                infoPanel.SetActive(true);
                infoPanel.transform.DOScale(Vector3.one / 4f, 0.1f);
            }    
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                infoPanel.transform.DOScale(Vector3.zero,0.1f).OnComplete(() =>
                {
                    infoPanel.SetActive(false);
                });
            }    
        }
    }
}
