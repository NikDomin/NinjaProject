using System;
using NewObjectPool;
using ObjectsPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Traps.SpawnTrap
{
    [Serializable]
    public class TrapMovementProjectile : MonoBehaviour, ISpawnTrap
    {
        public void Shoot(PoolMono<ProjectileTrigger> objectPool, Transform trapTransform, Direction direction)
        {
            var projectile = objectPool.GetFreeElement();
            
            projectile.transform.rotation = Quaternion.Euler(trapTransform.rotation.eulerAngles);
            projectile.transform.position = trapTransform.position;
            
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.DirectionVector = GameUtils.GetDirection(direction);
            
            // projectile.ProjectilePool = objectPool;
        }
    }
}