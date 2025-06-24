using DG.Tweening;
using Runtime.Audio;
using UnityEngine;

namespace Runtime.Objects.Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject rightDoor;

        private AudioSource _audioSource;
        private bool _isOpen;
        private Vector3 _leftDoorFirstPos;
        private Vector3 _rightDoorFirstPos;
        private void OnEnable()
        {
            _leftDoorFirstPos = leftDoor.transform.position;
            _rightDoorFirstPos = rightDoor.transform.position;
            _audioSource = GetComponentInParent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                leftDoor.transform.DOMove(_leftDoorFirstPos + new Vector3(0, 0, -2), 0.5f);
                rightDoor.transform.DOMove(_rightDoorFirstPos + new Vector3(0, 0, 2), 0.5f);
                AudioManager.Instance.PlaySfx(SfxType.DoorOpen, _audioSource);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                leftDoor.transform.DOMove(_leftDoorFirstPos, 0.5f);
                rightDoor.transform.DOMove(_rightDoorFirstPos, 0.5f);
                AudioManager.Instance.PlaySfx(SfxType.DoorOpen, _audioSource);
            }
        }
    }
}
