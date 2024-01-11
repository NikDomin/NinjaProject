using NewObjectPool;
using ObjectsPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Traps.SpawnTrap
{
    public class TrapFixedProjectile : MonoBehaviour, ISpawnTrap
    {
        public void Shoot( PoolMono<ProjectileTrigger> objectPool, Transform trapTransform, Direction direction)
        {
            var projectile = objectPool.GetFreeElement();
            
            projectile.transform.rotation = Quaternion.Euler(trapTransform.rotation.eulerAngles);
            projectile.transform.position = trapTransform.position + GameUtils.GetDirection(direction) * 1;
            // projectile.ProjectilePool = objectPool;
        }
    }
}