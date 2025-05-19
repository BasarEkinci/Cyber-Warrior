using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class ScrapContainer : MonoBehaviour
    {
        [SerializeField] private float scarpHeightBound;
        [SerializeField] private GameObject scrapStack;
        [SerializeField] private ScrapData scrapData;
        private int _previousScrapAmount;
        private void OnEnable()
        {
            _previousScrapAmount = scrapData.currentScarp;   
            scrapData.OnScrapAmountChanged += UpdateStackLevel;
        }
        
        private void UpdateStackLevel()
        {
            Vector3 scrapStackPos = scrapStack.transform.position;
            if (_previousScrapAmount > scrapData.currentScarp)
                scrapStackPos -= Vector3.up / 10f;
            else
                scrapStackPos += Vector3.up / 10f;

            scrapStackPos = new Vector3(scrapStackPos.x,Mathf.Clamp(scrapStack.transform.position.y,-0.25f,0.25f),scrapStackPos.z);
            scrapStack.transform.position = scrapStackPos;
            _previousScrapAmount = scrapData.currentScarp;
        }
        
        private void OnDisable()
        {
            scrapData.OnScrapAmountChanged -= UpdateStackLevel;
        }
    }
}
