using ObjectsPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Traps
{
    public enum Direction
    {
        down,
        right,
        up,
        left,
        upRight,
        upLeft,
        downRight,
        downLeft,
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

        private void OnValidate()
        {
            transform.rotation = GameUtils.GetRotation(GameUtils.GetDirection(balistaDirection));
        }

        public void ProjectileShoot()
        {
            var projectile = projectilePool.Get();
            
            projectile.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            projectile.transform.position = transform.position;
            
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.DirectionVector = GameUtils.GetDirection(balistaDirection);

            projectile.GetComponent<ProjectileTrigger>().ProjectilePool = projectilePool;

        }

        private void StartAttack()
        {
            animator.SetTrigger(ShootingKey);
        }
        
    }
}