using Data.UnityObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    public class ScrapContainer : MonoBehaviour
    {
        [SerializeField] private float scarpHeightBound;
        [SerializeField] private GameObject scrapStack;
        [FormerlySerializedAs("scrapData")] [SerializeField] private ScrapDataSO scrapDataSo;
        private int _previousScrapAmount;
        private void OnEnable()
        {
            _previousScrapAmount = scrapDataSo.currentScarp;   
            scrapDataSo.OnScrapAmountChanged += UpdateStackLevel;
        }
        
        private void UpdateStackLevel()
        {
            Vector3 scrapStackPos = scrapStack.transform.position;
            if (_previousScrapAmount > scrapDataSo.currentScarp)
                scrapStackPos -= Vector3.up / 10f;
            else
                scrapStackPos += Vector3.up / 10f;

            scrapStackPos = new Vector3(scrapStackPos.x,Mathf.Clamp(scrapStack.transform.position.y,-0.25f,0.25f),scrapStackPos.z);
            scrapStack.transform.position = scrapStackPos;
            _previousScrapAmount = scrapDataSo.currentScarp;
        }
        
        private void OnDisable()
        {
            scrapDataSo.OnScrapAmountChanged -= UpdateStackLevel;
        }
    }
}
