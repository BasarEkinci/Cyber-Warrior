using UnityEngine;

namespace Runtime.MainMenu
{
    public class Companion : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask lookAtLayerMask;
        [SerializeField] private Vector3 offset;

        private void Update()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, lookAtLayerMask))
            {
                Vector3 lookPoint = hit.point;
                transform.LookAt(lookPoint + offset);
            }
        }
    }
}
