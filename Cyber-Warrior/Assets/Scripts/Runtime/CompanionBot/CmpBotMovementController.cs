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
    private Vector3 _followOffset;
    private Vector3 _currentOffset;
    private Vector3 _attackOffset;
    private Transform _currentTarget;
    private Rotator _rotator;

    private void OnEnable()
    {
        _moveSpeed = data.movementData.MoveSpeed;
        _followOffset = data.movementData.FollowOffset;
        _attackOffset = data.movementData.AttackOffset;
        modeEvent.OnEventRaised += OnBotModeChange;
        transformEvent.OnEventRaised += OnTargetChange;
        _currentTarget = followPos;
        _rotator = new Rotator(transform,cross);
    }

    private void OnTargetChange(Transform currentTarget)
    {
        
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
                _currentTarget = followPos;
                
                break;
            case CmpMode.Attacker:
                _currentOffset = _attackOffset;
                _currentTarget = attackPos;
                break;
            default:
                _currentOffset = _followOffset;
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
        FollowTarget(_moveSpeed * Time.fixedDeltaTime,_currentTarget);
    }

    private void FollowTarget(float speed,Transform target)
    {
        Vector3 desiredPos = target.position + _currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed);
    }
}
