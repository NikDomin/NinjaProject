using System.Collections;
using ObjectsPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundFxManager : MonoBehaviour
    {
        public static SoundFxManager instance;

        [SerializeField] private AudioSource soundFxObject;
        private GameObjectPool audioSourcePool;

        private Coroutine destroyCoroutine;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            audioSourcePool = new GameObjectPool(soundFxObject.gameObject, 5);
        }

        private void OnDisable()
        {
            StopCoroutine(destroyCoroutine);
        }

        public void PlaySoundFxClip(AudioClip[] audioClips, float volume)
        {
            int random = Random.Range(0, audioClips.Length);

            AudioSource audioSource = audioSourcePool.Get().GetComponent<AudioSource>();
            
            audioSource.clip = audioClips[random];
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            destroyCoroutine = StartCoroutine(DelayReturnFxObject(clipLength, audioSource.gameObject));
        }

        private IEnumerator DelayReturnFxObject(float time, GameObject gmObject)
        {
            yield return new WaitForSeconds(time);
            audioSourcePool.Return(gmObject);
        }
    }
}