using System;
using Agent;
using Traps;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Movement
{
    [RequireComponent(typeof(AgentBoxDetection))]
    public class BoostComponent:MonoBehaviour
    {
        public UnityEvent OnBoost;
        
        [SerializeField] private float boostStrength;
        [SerializeField] private Direction boostDirection;

        private AgentBoxDetection boxDetection;

        #region Mono

        private void Awake()
        {
            boxDetection = GetComponent<AgentBoxDetection>();
        }
        
        private void OnValidate()
        {
            transform.rotation = GameUtils.GetRotation(GameUtils.GetDirection(boostDirection));
        }

        private void FixedUpdate()
        {
            Detection();
        }

        #endregion

        private void Detection()
        {
            int boxCount = boxDetection.OverlapBoxNonAlloc();
            if (boxCount == null || boxCount == 0)
                return;
        
            foreach (var item in boxDetection.Buffer)
            {
                if(item == null)
                    continue;
                if (item.TryGetComponent(out Rigidbody2D _rigidbody2D))
                {
                    float zRotation = transform.rotation.eulerAngles.z;
                    Vector2 movementDirection = new Vector2(Mathf.Cos(zRotation * Mathf.Deg2Rad), Mathf.Sin(zRotation * Mathf.Deg2Rad));
                    _rigidbody2D.velocity = new Vector2(
                        _rigidbody2D.velocity.x + movementDirection.x * boostStrength,
                        _rigidbody2D.velocity.y + movementDirection.y * boostStrength);
                    
                    OnBoost?.Invoke();
                }
            }
        }

    }
}