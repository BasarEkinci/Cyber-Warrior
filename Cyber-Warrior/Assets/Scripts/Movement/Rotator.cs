using DG.Tweening;
using UnityEngine;

namespace Movement
{
    public class Rotator
    {
        private readonly Transform _currentTransform;
        private readonly GameObject _target;

        public Rotator(Transform currentTransform, GameObject target)
        {
            _currentTransform = currentTransform;
            _target = target;
        }

        public void RotateToTarget(GameObject aimTarget,float rotationSpeed,Vector3 offset = default)
        {
            Vector3 directionToAim = aimTarget.transform.position + offset - _currentTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToAim);
            Quaternion newRotation = Quaternion.RotateTowards(_currentTransform.rotation, targetRotation, rotationSpeed);
            _currentTransform.rotation = newRotation;
        }
        
        /// <summary>
        /// Sets the object's look direction towards the target.
        /// </summary>
        public void SetLookDirection()
        {
            if (_target == null) return;

            Vector3 directionToAim = _target.transform.position - _currentTransform.position;
            directionToAim.y = 0f;

            Vector3 currentForward = _currentTransform.forward;
            currentForward.y = 0f;

            float angle = Vector3.SignedAngle(currentForward.normalized, directionToAim.normalized, Vector3.up);

            if (angle > 45f)
            {
                RotateObject(90f);
            }
            else if (angle < -45f)
            {
                RotateObject(-90f);
            }
        }
        /// <summary>
        /// To fix the rotation of the object, sets the rotation to the nearest 90 degrees.
        /// </summary>
        /// <param name="yAngleDelta"></param>
        private void RotateObject(float yAngleDelta)
        {
            Vector3 newRotation = _currentTransform.rotation.eulerAngles + new Vector3(0f, yAngleDelta, 0f);
            _currentTransform.DORotate(newRotation, 0.1f).OnComplete(() =>
            {
                float currentYRotation = _currentTransform.rotation.eulerAngles.y;
                float normalizedY = Mathf.DeltaAngle(0f, currentYRotation);
                float snapY = Mathf.Round(normalizedY / 90f) * 90f;
                Vector3 newEuler = new Vector3(0f, snapY, 0f);
                _currentTransform.rotation = Quaternion.Euler(newEuler);
            });
        }
    }
}