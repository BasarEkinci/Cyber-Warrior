using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject rightDoor;

        private bool _isOpen;
        private Vector3 _leftDoorFirstPos;
        private Vector3 _rightDoorFirstPos;
        private void OnEnable()
        {
            _leftDoorFirstPos = leftDoor.transform.position;
            _rightDoorFirstPos = rightDoor.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                leftDoor.transform.DOMove(_leftDoorFirstPos + new Vector3(0, 0, -2), 0.5f);
                rightDoor.transform.DOMove(_rightDoorFirstPos + new Vector3(0, 0, 2), 0.5f);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                leftDoor.transform.DOMove(_leftDoorFirstPos, 0.5f);
                rightDoor.transform.DOMove(_rightDoorFirstPos, 0.5f);
            }
        }
    }
}
