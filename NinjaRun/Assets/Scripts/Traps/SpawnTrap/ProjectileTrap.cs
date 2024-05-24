using System;
using NewObjectPool;
using ObjectsPool;
using Projectiles;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Traps.SpawnTrap
{
    
    public interface ISpawnTrap
    {
        public void Shoot(PoolMono<ProjectileTrigger> objectPool, Transform trapTransform, Direction direction);
    }
    public class ProjectileTrap : MonoBehaviour
    {
        [SerializeField] private ProjectileTrigger objectToSpawn;
        [SerializeField] private float reloadTimeSeconds;
        [SerializeField] private Direction trapDirection;
        [SerializeField] private int poolPreloadCount = 1;
        [SerializeField] private bool poolAutoExpand;
        
        private ISpawnTrap spawnTrap;
        private PoolMono<ProjectileTrigger> projectilePool;
        private Animator animator;

        private GameOverPanel gameOverPanel;
        
        
        private readonly int ShootingKey = Animator.StringToHash("Shooting");

        #region MONO

        private void Awake()
        {
            gameOverPanel = FindObjectOfType<GameOverPanel>(true);
            
            projectilePool = new PoolMono<ProjectileTrigger>(objectToSpawn, poolPreloadCount);
            projectilePool.autoExpand = poolAutoExpand;
            
            animator = GetComponent<Animator>();
            spawnTrap = GetComponent<ISpawnTrap>();
            
            InvokeRepeating(nameof(StartAttack), 1f, reloadTimeSeconds);
        }

        private void OnEnable()
        {
            gameOverPanel.OnEndResetLevel += Reset;
        }

        private void OnDisable()
        {
            gameOverPanel.OnEndResetLevel -= Reset;
        }
        private void OnValidate()
        {
            transform.rotation = GameUtils.GetRotation(GameUtils.GetDirection(trapDirection));
        }

        #endregion

        private void Reset()
        {
            CancelInvoke(nameof(StartAttack));
              
            projectilePool = new PoolMono<ProjectileTrigger>(objectToSpawn, poolPreloadCount);
            projectilePool.autoExpand = poolAutoExpand;
            
            InvokeRepeating(nameof(StartAttack), 1f, reloadTimeSeconds);
        }


        public void ProjectileShoot()
        {
           spawnTrap.Shoot(projectilePool,transform,trapDirection);
        }

        private void StartAttack()
        {
            animator.SetTrigger(ShootingKey);
        }

    }
}