using ObjectsPool;
using UnityEngine;

namespace Level
{
    public class EffectsHandler : MonoBehaviour
    {
        public static EffectsHandler Instance;

        public GameObjectPool HitParticlesPool;

        [SerializeField]private GameObject hitParticleSystem;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            HitParticlesPool = new GameObjectPool(hitParticleSystem, 3);
        }

        public void EnableHitParticle(Vector2 position)
        {
            var particle = HitParticlesPool.Get();
            if (particle.TryGetComponent(out ParticleSystem particleSystem))
            {
                particle.transform.position = position;
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
                
                Debug.Log(particleSystem.time);
            }
        }
        
    }
}