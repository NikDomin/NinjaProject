using System;
using ObjectsPool;
using Projectiles;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Traps.SpawnTrap
{
    
    public interface ISpawnTrap
    {
        public void Shoot(GameObjectPool objectPool, Transform trapTransform, Direction direction);
    }
    public class ProjectileTrap : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float reloadTimeSeconds;
        [SerializeField] private Direction trapDirection;
        [SerializeField] private int poolPreloadCount = 1; 
        
        private ISpawnTrap spawnTrap;
        private GameObjectPool objectPool;
        private Animator animator;
        
        
        private readonly int ShootingKey = Animator.StringToHash("Shooting");
        
        private void Awake()
        {
            objectPool = new GameObjectPool(objectToSpawn, poolPreloadCount);
            animator = GetComponent<Animator>();
            spawnTrap = GetComponent<ISpawnTrap>();
            
            InvokeRepeating(nameof(StartAttack), 1f, reloadTimeSeconds);
        }

        private void OnDisable()
        {
            try
            {
                objectPool.ReturnAll();
            }
            catch (Exception e)
            {
                Debug.LogError($"Disabling object pool failed at game object {gameObject.name}: {e.Message}");
            }
        }

        private void OnValidate()
        {
            transform.rotation = GameUtils.GetRotation(GameUtils.GetDirection(trapDirection));
        }

        public void ProjectileShoot()
        {
           spawnTrap.Shoot(objectPool,transform,trapDirection);
        }

        private void StartAttack()
        {
            animator.SetTrigger(ShootingKey);
        }

    }
}