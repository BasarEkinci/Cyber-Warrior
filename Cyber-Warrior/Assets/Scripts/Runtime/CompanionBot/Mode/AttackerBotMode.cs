using Runtime.Audio;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class AttackerBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Action;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }
        
        private CmpBotEffectManager _effectManager;
        private CmpCombatData _combatData;
        private Transform _parent;
        private AudioSource _audioSource;

        private float _attackTimer;
        private float _detectionTimer;
        private const float DetectionInterval = 0.3f;

        #region Overridden Methods

        private void Awake()
        {
            _parent = transform.parent;
        }

        public override void Initialize()
        {
            if (_effectManager == null)
                _effectManager = _parent.GetComponentInChildren<CmpBotEffectManager>(true);

            if (TargetObject == null)
                TargetObject = anchorPoints.GetInitialTargetObject();

            if (FollowPosition == null)
                FollowPosition = anchorPoints.GetAnchorPoint(mode);

            if (eyeMaterial != null)
                eyeMaterial.color = modeColor;

            _audioSource = GetComponentInParent<AudioSource>();
            _combatData = GetDataAtCurrentLevel().combatData;
        }

        public override void Execute()
        {
            _detectionTimer += Time.deltaTime;

            if (_detectionTimer >= DetectionInterval)
            {
                DetectAndSetNearestEnemy();
                _detectionTimer = 0f;
            }
            
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _combatData.attackCooldown)
            {
                Attack();
                _attackTimer = 0f;
            }
        }

        public override void ExitState()
        {
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void Move(Transform currentTransform, float deltaTime)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * deltaTime);
        }
        #endregion

        #region Bot Logic
        private CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[GameDatabaseManager.Instance.LoadData(SaveKeys.CompanionLevel)];
        }

        private void Attack()
        {
            if (_effectManager == null)
                _effectManager = _parent.GetComponentInChildren<CmpBotEffectManager>(true);
            
            if (TargetObject.TryGetComponent(out IDamageable damageable))
            {
                _effectManager.PlayFireEffect();
                AudioManager.Instance.PlaySfx(SfxType.BotAttack,_audioSource);
                damageable.TakeDamage(_combatData.damage);
            }
        }

        private void DetectAndSetNearestEnemy()
        {
            Transform nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && nearestEnemy != TargetObject)
            {
                TargetObject = nearestEnemy;
                return;
            }
            if (nearestEnemy == null)
            {
                TargetObject = anchorPoints.GetInitialTargetObject();
            }
        }
        
        private Transform FindNearestEnemy()
        {
            Collider[] hits = Physics.OverlapSphere(
                _parent.position,
                _combatData.range,
                _combatData.enemyLayer,
                QueryTriggerInteraction.Collide);

            Transform nearest = null;
            float minSqrDist = float.MaxValue;
            Vector3 selfPos = transform.position;

            foreach (var hit in hits)
            {
                float sqrDist = (hit.transform.position - selfPos).sqrMagnitude;
                if (sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    nearest = hit.transform;
                }
            }
            return nearest;
        }

        #endregion

#if UNITY_EDITOR
        #region Editor Gizmos

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _combatData.range);
        }

        #endregion
#endif
    }
}
