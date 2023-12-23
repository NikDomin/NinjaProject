using System.Collections;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(ProjectileTrigger))]
    public class ProjectileTimer : MonoBehaviour
    {
        [SerializeField] private int timeToLive;
        private ProjectileTrigger projectileTrigger;
        
        private void Awake()
        {
            projectileTrigger = GetComponent<ProjectileTrigger>();
        }
        
        private void OnEnable()
        {
            StartCoroutine(DelayReturnObject());
        }

        private IEnumerator DelayReturnObject()
        {
            yield return new WaitForSeconds(timeToLive);
            projectileTrigger.ProjectilePool.Return(gameObject);
        }
    }
}