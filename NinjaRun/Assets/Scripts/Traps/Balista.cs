using System;
using Assets.Scripts.ObjectsPool;
using ObjectsPool;
using Projectiles;
using UnityEngine;

namespace Traps
{
    enum Direction
    {
        down,
        right,
        up,
        left
    }
    public class Balista: MonoBehaviour
    {
        [SerializeField] private GameObject arrow;
        [SerializeField] private float reloadTime;
        [SerializeField] private Direction balistaDirection;
        
        private Animator animator;
        private GameObjectPool projectilePool;
        
        
        private readonly int ShootingKey = Animator.StringToHash("Shooting");
        
        private void Awake()
        {
            projectilePool = new GameObjectPool(arrow, 4);
            animator = GetComponent<Animator>();
            
            InvokeRepeating(nameof(StartAttack), 1f, reloadTime);
        }



        // private void OnDisable()
        // {
        //     projectilePool.ReturnAll();
        // }

        public void ProjectileShoot()
        {
            var projectile = projectilePool.Get();
            
            projectile.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            projectile.transform.position = transform.position;
            
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.DirectionVector = GetProjectileDirection(projectileMovement.DirectionVector);

            projectile.GetComponent<ProjectileTrigger>().ProjectilePool = projectilePool;

        }

        private void StartAttack()
        {
            animator.SetTrigger(ShootingKey);
        }

        private Vector3 GetProjectileDirection(Vector3 startDirection)
        {
            if (balistaDirection == Direction.down)
                startDirection = Vector3.down;
            else if (balistaDirection == Direction.right)
                startDirection = Vector3.right;
            else if (balistaDirection == Direction.up)
                startDirection = Vector3.up;
            else if (balistaDirection == Direction.left)
                startDirection = Vector3.left;
            return startDirection;
        }
    }
}