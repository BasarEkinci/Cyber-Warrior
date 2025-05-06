using UnityEngine;

namespace Combat.Componenets
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        private Camera _cam;
        private void OnEnable()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask)) 
            { 
                Vector3 mousePosition = hit.point;
                transform.position = new Vector3(mousePosition.x,1f,mousePosition.z);
            }
        }
    }
}
