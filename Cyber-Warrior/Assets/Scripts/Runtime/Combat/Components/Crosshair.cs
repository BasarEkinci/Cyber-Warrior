using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Combat.Components
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float yPositionOffset = 1f;
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private GameStateEvent gameStateEvent;
        
        private Camera _cam;
        private Tween _tween;
        private bool _canMove = true;
        private void OnEnable()
        {
            playerDeathEvent.OnEventRaised += OnPlayerDeath;
            gameStateEvent.OnEventRaised += OnStateChange;
            _cam = Camera.main;
            _tween = transform.DORotate(Vector3.up * 90, 1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
            _tween.Play();
        }

        private void OnStateChange(GameState arg0)
        {
            if (arg0 == GameState.GameOver || arg0 == GameState.Pause)
            {
                _canMove = false;
                _tween.Kill();
                transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear);
            }
            else if (arg0 == GameState.Base || arg0 == GameState.Action)
            {
                _canMove = true;
                _tween.Play();
                transform.DOScale(Vector3.one * 0.4f, 0.1f).SetEase(Ease.Linear);
            }
        }

        private void OnPlayerDeath()
        {
            _tween.Kill();
            transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear);
        }

        private void Update()
        {
            if (!_canMove) return;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask)) 
            { 
                Vector3 mousePosition = hit.point;
                transform.position = new Vector3(mousePosition.x,yPositionOffset,mousePosition.z);
            }
        }

        private void OnDisable()
        {
            playerDeathEvent.OnEventRaised -= OnPlayerDeath;
            gameStateEvent.OnEventRaised -= OnStateChange;
            _tween.Kill();
        }
    }
}
