using ObjectsPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Traps.SpawnTrap
{
    public class TrapFixedProjectile : MonoBehaviour, ISpawnTrap
    {
        public void Shoot(GameObjectPool objectPool, Transform trapTransform, Direction direction)
        {
            var projectile = objectPool.Get();
            
            projectile.transform.rotation = Quaternion.Euler(trapTransform.rotation.eulerAngles);
            projectile.transform.position = trapTransform.position + GameUtils.GetDirection(direction) * 1;
            projectile.GetComponent<ProjectileTrigger>().ProjectilePool = objectPool;
        }
    }
}