using Level;

namespace Agent.Enemy
{
    public class EnemyHealth:Hittable
    {
        
        public override void GetHit()
        {
            OnDead?.Invoke();
            EffectsHandler.Instance.EnableHitParticle(transform.position);
            gameObject.SetActive(false);
        }
    }
}