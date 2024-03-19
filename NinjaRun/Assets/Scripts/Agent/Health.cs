using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Agent
{
    public class Health : MonoBehaviour
    {
        public UnityEvent OnDead;
        [SerializeField] private AudioClip[] damageSoundClips;
        
        public void GetHit()
        {
            OnDead.Invoke();
            
            PlayDamageSoundFX();
            
            gameObject.SetActive(false);
        }

        private void PlayDamageSoundFX()
        {
            if (damageSoundClips == null)
            {
                Debug.LogWarning("Audio clip not assigned/ GameObject:" + gameObject.name);
                return;
            }
            SoundFxManager.instance.PlaySoundFxClip(damageSoundClips, 1f);
        }
    }
}