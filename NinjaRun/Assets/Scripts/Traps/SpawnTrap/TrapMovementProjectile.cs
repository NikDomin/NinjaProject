using System;
using ObjectsPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Traps.SpawnTrap
{
    [Serializable]
    public class TrapMovementProjectile : MonoBehaviour, ISpawnTrap
    {
        public void Shoot(GameObjectPool objectPool, Transform trapTransform, Direction direction)
        {
            var projectile = objectPool.Get();
            
            projectile.transform.rotation = Quaternion.Euler(trapTransform.rotation.eulerAngles);
            projectile.transform.position = trapTransform.position;
            
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.DirectionVector = GameUtils.GetDirection(direction);
            
            projectile.GetComponent<ProjectileTrigger>().ProjectilePool = objectPool;
        }
    }
}