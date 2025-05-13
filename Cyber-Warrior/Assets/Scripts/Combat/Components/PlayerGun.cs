using Inputs;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat.Components
{
    public class PlayerGun : MonoBehaviour
    {
        [FormerlySerializedAs("gunFireEventSo")] [SerializeField] private HoldInputChannelSO holdInputChannelSo;
        private IPlayerInput _inputReader;
        private bool _isFiring;
        private void OnEnable()
        {
            _inputReader = new InputReader(holdInputChannelSo);
            holdInputChannelSo.OnFireStart += OnFireStart;
            holdInputChannelSo.OnFireEnd += OnFireEnd;
        }

        private void OnFireEnd()
        {
            _isFiring = false;
            Debug.Log("Fire Ended");
        }

        private void OnFireStart() 
        {
            _isFiring = true;
            Debug.Log("Fire Started");
        }

        private void Update()
        {
            Debug.Log(_isFiring);
        }

        private void OnDisable()
        {
            holdInputChannelSo.OnFireStart -= OnFireStart;
            holdInputChannelSo.OnFireEnd -= OnFireEnd;
            if (_inputReader is InputReader disposableInput)
            {
                disposableInput.Dispose();
            }
        }
    }
}