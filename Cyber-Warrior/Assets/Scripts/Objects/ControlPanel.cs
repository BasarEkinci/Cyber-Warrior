using Managers;
using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject cmpInfoPanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                cmpInfoPanel.SetActive(true);
            }    
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                cmpInfoPanel.SetActive(false);
            }    
        }
        
    }
}
