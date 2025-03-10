using System;
using UnityEngine;

namespace Agent.Enemy.EnemyMovement
{
    public abstract class EnemyAbstractMovement: MonoBehaviour
    {
        [HideInInspector] public bool IsCanMove;
        
        public Action<Vector2> OnChangeDirectionVector;
        
        public virtual void Movement()
        {
            
        }
    }
}