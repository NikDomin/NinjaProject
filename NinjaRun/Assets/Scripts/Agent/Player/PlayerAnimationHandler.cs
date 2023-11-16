using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Player
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        public event Action OnAttack;

        private void AttackActionTrigger() => OnAttack?.Invoke();

        //timed
        public void TestActionTrigger() => OnAttack?.Invoke();
    }
}