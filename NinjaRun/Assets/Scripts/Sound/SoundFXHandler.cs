using UnityEngine;

namespace Sound
{
    public class SoundFXHandler :MonoBehaviour
    {
        [SerializeField] private AudioClip[] SFXClips;


        public void Play()
        {
            PlaySoundFX(SFXClips);
        }
        
        
        private void PlaySoundFX(AudioClip[] audioClips)
        {
            if (audioClips == null || audioClips.Length == 0)
            {
                Debug.LogWarning("Audio clip not assigned/ GameObject:" + gameObject.name);
                return;
            }
            SoundFxManager.instance.PlaySoundFxClip(audioClips, 1f);
        }
    }
}