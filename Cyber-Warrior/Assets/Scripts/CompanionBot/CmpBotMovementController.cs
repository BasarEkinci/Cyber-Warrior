using Data.UnityObjects;
using Enums;
using UnityEngine;

public class CmpBotMovementController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform target;

    [Header("Data")]
    [SerializeField] private CmpBotDataSO data;
    [SerializeField] private BotModeEvent modeEvent;
    [SerializeField] private TransformEventChannel transformEvent;

    private float _moveSpeed;
    private Vector3 _followOffset;
    private Vector3 _currentOffset;
    private Vector3 _attackOffset;

    private void OnEnable()
    {
        _moveSpeed = data.movementData.MoveSpeed;
        _followOffset = data.movementData.FollowOffset;
        _attackOffset = data.movementData.AttackOffset;
        modeEvent.OnEventRaised += OnBotModeChange;
        transformEvent.OnEventRaised += OnTargetChange;
    }

    private void OnTargetChange(Transform currentTarget)
    {
        target = currentTarget;
    }

    private void OnBotModeChange(CmpMode mode)
    {
        switch (mode)
        {
            case CmpMode.Base:
                _currentOffset = Vector3.zero;
                break;
            case CmpMode.Healer:
                _currentOffset = _followOffset;
                break;
            case CmpMode.Attacker:
                _currentOffset = _attackOffset;
                break;
            default:
                _currentOffset = _followOffset;
                break;
        }
    }

    private void FixedUpdate()
    {
        FollowTarget(_moveSpeed * Time.fixedDeltaTime);
    }

    private void FollowTarget(float speed)
    {
        Vector3 desiredPos = target.position + _currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed);
    }
}
