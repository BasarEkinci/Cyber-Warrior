using Data.UnityObjects;
using Enums;
using Movement;
using UnityEngine;

public class CmpBotMovementController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform followPos;
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject cross;

    [Header("Data")]
    [SerializeField] private CmpBotDataSO data;
    [SerializeField] private BotModeEvent modeEvent;
    [SerializeField] private TransformEventChannel transformEvent;

    private float _moveSpeed;
    private Transform _currentFollowPos;
    private Transform _currentAimPos;
    private Rotator _rotator;

    private void OnEnable()
    {
        _moveSpeed = data.movementData.MoveSpeed;
        modeEvent.OnEventRaised += OnBotModeChange;
        transformEvent.OnEventRaised += OnFollowPosChange;
        _currentFollowPos = followPos;
        _rotator = new Rotator(transform,cross);
    }

    private void OnFollowPosChange(Transform newAimPos)
    {
        _currentAimPos = newAimPos;
    }

    private void OnBotModeChange(CmpMode mode)
    {
        switch (mode)
        {
            case CmpMode.Base:
                _currentFollowPos = followPos; //change it to base pos
                _currentAimPos = cross.transform; //change it to base aim pos
                break;
            case CmpMode.Healer:
                _currentFollowPos = followPos;
                _currentAimPos = cross.transform;
                break;
            case CmpMode.Attacker:
                _currentFollowPos = attackPos;
                break;
            default:
                _currentFollowPos = followPos;
                break;
        }
    }
    private void Update()
    {
        _rotator.SetLookDirection();
        transform.LookAt(cross.transform);
    }
    private void FixedUpdate()
    {
        FollowTarget(_moveSpeed * Time.fixedDeltaTime,_currentFollowPos);
    }
    private void SetAimPos()
    {
        if (_currentAimPos == null)
        {
            _currentAimPos = cross.transform;
        }
    }
    private void FollowTarget(float speed,Transform target)
    {
        Vector3 desiredPos = target.position;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed);
    }
}
