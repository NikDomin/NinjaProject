using System;
using UnityEngine;

namespace Agent.Enemy
{
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public event Action OnDisableDeadBody;
        public event Action OnEndExclamationPoint;
        public event Action OnEndAttack;
        public event Action OnMoveAttack;
        public event Action OnAttack;

        private void DisableDeadBodyTrigger() => OnDisableDeadBody?.Invoke();
        private void EndExclamationPointTrigger() => OnEndExclamationPoint?.Invoke();
        private void EndAttackTrigger() => OnEndAttack?.Invoke();
        private void MoveAttackTrigger() => OnMoveAttack?.Invoke();
        private void AttackTrigger() => OnAttack?.Invoke();
    }
}