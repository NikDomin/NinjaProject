using System.Collections;
using Level;
using Movement;
using UnityEngine;

namespace Agent
{
    public class PlayerHealth : Hittable
    {
        // public UnityEvent OnDead;
        private bool isInvulnerability = false;

        private NewSwipeDetection newSwipeDetection;

        #region Mono

        private void Awake()
        {
            newSwipeDetection = GetComponent<NewSwipeDetection>();
        }

        private void OnEnable()
        {
            newSwipeDetection.OnSwipe += PlayerSwipe;
        }

        private void OnDisable()
        {
            newSwipeDetection.OnSwipe -= PlayerSwipe;

        }

        #endregion


        public override void GetHit()
        {
            if (isInvulnerability)
                return;
            
            OnDead.Invoke();
            EffectsHandler.Instance.EnableHitParticle(transform.position);
            gameObject.SetActive(false);
            DeathWall.deathWall.CanDisableLevelParts = false;
        }
        public void SetInvulnerability(float time)
        {
            isInvulnerability = true;
            StartCoroutine(Timer(time));
        }

        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            isInvulnerability = false;
        }
        private void PlayerSwipe()
        {
            if (isInvulnerability)
            {
                Debug.Log("Timer coroutine stopped");
                StopCoroutine(Timer(2f));
                isInvulnerability = false;
            }
        }

    }
}
