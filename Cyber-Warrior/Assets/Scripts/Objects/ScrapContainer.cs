using Data.UnityObjects;
using UnityEngine;

namespace Objects
{
    public class ScrapContainer : MonoBehaviour
    {
        [SerializeField] private float scarpHeightBound;
        [SerializeField] private GameObject scrapStack;
        private int _previousScrapAmount;
        private void OnEnable()
        {
            _previousScrapAmount = ScarpAmountManager.Instance.currentScarp;   
            ScarpAmountManager.Instance.onScrapAmountChanged.AddListener(UpdateStackLevel);
        }
        
        private void UpdateStackLevel()
        {
            Vector3 scrapStackPos = scrapStack.transform.position;
            if (_previousScrapAmount > ScarpAmountManager.Instance.currentScarp)
                scrapStackPos -= Vector3.up / 10f;
            else
                scrapStackPos += Vector3.up / 10f;

            scrapStackPos = new Vector3(scrapStackPos.x,Mathf.Clamp(scrapStack.transform.position.y,-0.25f,0.25f),scrapStackPos.z);
            scrapStack.transform.position = scrapStackPos;
            _previousScrapAmount = ScarpAmountManager.Instance.currentScarp;
        }
        
        private void OnDisable()
        {
            ScarpAmountManager.Instance.onScrapAmountChanged.RemoveListener(UpdateStackLevel);
        }
    }
}
