using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public event Action OnDisableDeadBody;
        public event Action OnEndExclamationPoint;

        private void DisableDeadBodyTrigger() => OnDisableDeadBody?.Invoke();
        private void EndExclamationPointTrigger() => OnEndExclamationPoint?.Invoke();

    }
}