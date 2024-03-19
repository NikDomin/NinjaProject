using UnityEngine;

namespace Sound
{
    public class SoundFxManager : MonoBehaviour
    {
        public static SoundFxManager instance;

        [SerializeField] private AudioSource soundFxObject;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void PlaySoundFxClip(AudioClip[] audioClips, float volume)
        {
            int random = Random.Range(0, audioClips.Length);
            
            AudioSource audioSource = Instantiate(soundFxObject);
            audioSource.clip = audioClips[random];
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}