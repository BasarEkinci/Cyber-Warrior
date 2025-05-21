using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                infoPanel.SetActive(true);
            }    
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                infoPanel.SetActive(false);
            }    
        }
    }
}
