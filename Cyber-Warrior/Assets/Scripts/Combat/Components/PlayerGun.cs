using Inputs;
using ScriptableObjects.Events;
using UnityEngine;

namespace Combat.Components
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private GunFireEventSO gunFireEventSo;
        private IPlayerInput _inputReader;
        private bool _isFiring;
        private void OnEnable()
        {
            _inputReader = new InputReader(gunFireEventSo);
            gunFireEventSo.OnFireStart += OnFireStart;
            gunFireEventSo.OnFireEnd += OnFireEnd;
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
            gunFireEventSo.OnFireStart -= OnFireStart;
            gunFireEventSo.OnFireEnd -= OnFireEnd;
            if (_inputReader is InputReader disposableInput)
            {
                disposableInput.Dispose();
            }
        }
    }
}