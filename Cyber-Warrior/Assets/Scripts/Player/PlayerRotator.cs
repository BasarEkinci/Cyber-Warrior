using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerRotator
    {
        private readonly Transform _playerTransform;
        private readonly GameObject _crosshair;

        public PlayerRotator(Transform playerTransform, GameObject crosshair)
        {
            _playerTransform = playerTransform;
            _crosshair = crosshair;
        }

        public void LookAtAim()
        {
            if (_crosshair == null) return;

            Vector3 directionToAim = _crosshair.transform.position - _playerTransform.position;
            directionToAim.y = 0f;

            Vector3 currentForward = _playerTransform.forward;
            currentForward.y = 0f;

            float angle = Vector3.SignedAngle(currentForward.normalized, directionToAim.normalized, Vector3.up);

            if (angle > 45f)
            {
                RotatePlayer(90f);
            }
            else if (angle < -45f)
            {
                RotatePlayer(-90f);
            }
        }

        private void RotatePlayer(float yAngleDelta)
        {
            Vector3 newRotation = _playerTransform.rotation.eulerAngles + new Vector3(0f, yAngleDelta, 0f);
            _playerTransform.DORotate(newRotation, 0.1f).OnComplete(() =>
            {
                float currentYRotation = _playerTransform.rotation.eulerAngles.y;
                float normalizedY = Mathf.DeltaAngle(0f, currentYRotation);
                float snapY = Mathf.Round(normalizedY / 90f) * 90f;
                Vector3 newEuler = new Vector3(0f, snapY, 0f);
                _playerTransform.rotation = Quaternion.Euler(newEuler);
            });
        }
    }
}