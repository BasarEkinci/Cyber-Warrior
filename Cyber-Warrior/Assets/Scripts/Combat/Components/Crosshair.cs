using DG.Tweening;
using UnityEngine;

namespace Combat.Components
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float yPositionOffset = 1f;
        private Camera _cam;
        private Tween _tween;
        private void OnEnable()
        {
            _cam = Camera.main;
            _tween = transform.DORotate(Vector3.up * 90, 1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
            _tween.Play();
        }

        private void Update()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask)) 
            { 
                Vector3 mousePosition = hit.point;
                transform.position = new Vector3(mousePosition.x,yPositionOffset,mousePosition.z);
            }
        }

        private void OnDisable()
        {
            _tween.Kill();
        }
    }
}
