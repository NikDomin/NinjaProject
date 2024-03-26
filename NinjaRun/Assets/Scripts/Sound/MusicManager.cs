using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private SoundTrack[] soundTracks;
        [SerializeField] private bool isChangeSoundTrack;
        private AudioSource audioSource;
        private Coroutine changeSongCoroutine;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            PlayRandomSoundTrack();
        }

        private void OnDisable()
        {
            StopCoroutine(changeSongCoroutine);
        }

        [ContextMenu("Random Sound Track")]
        private async void PlayRandomSoundTrack()
        {
            float totalChance = 0f;
            foreach (var item in soundTracks)
            {
                totalChance += item.SongChance;
            }

            float rand = Random.Range(0, totalChance);
            float cumulativeChance = 0f;

            foreach (var item in soundTracks)
            {
                cumulativeChance += item.SongChance;

                if (rand <= cumulativeChance)
                {
                    //play sound
                    // string path = Path.Combine("Sound/", item.AudioClipName);
                    // audioSource.clip = Resources.Load<AudioClip>(path);
                    // audioSource.Play();

                    AudioClip audioClip = await LoadAudioClip(item.AudioClipName);
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    if (isChangeSoundTrack)
                    {
                        changeSongCoroutine = StartCoroutine(DelayChangeSoundTrack(audioSource.clip.length, item));
                    }
                    Debug.Log("Change audio clip end");
                    
                    return;
                }
            }
        }

        private async void PlaySoundTrack(SoundTrack soundTrack)
        {
            AudioClip audioClip = await LoadAudioClip(soundTrack.AudioClipName);
            audioSource.clip = audioClip;
            audioSource.Play();
            if (isChangeSoundTrack)
            {
                changeSongCoroutine = StartCoroutine(DelayChangeSoundTrack(audioSource.clip.length, soundTrack));
            }
            Debug.Log("Change audio clip end");
        }


        private async Task<AudioClip> LoadAudioClip(string clipName)
        {
            string path = Path.Combine("Sound/", clipName);
            AudioClip audioClip = await Task.FromResult(Resources.Load<AudioClip>(path));
            return audioClip;
        }

        private IEnumerator DelayChangeSoundTrack(float time, SoundTrack soundTrack)
        {
            yield return new WaitForSeconds(time);
            float random = Random.Range(0f, 1f);
            if (random >= soundTrack.SongReplayChance)
            {
                PlayRandomSoundTrack();
            }
            else
            {
               PlaySoundTrack(soundTrack);
            }
        }

    }
    
    [Serializable]
    public class SoundTrack
    {
        public string AudioClipName;

        [Space(20f)] 
        [Range(0f, 1f)] public float SongChance = 0.5f;
        [Range(0f, 1f)] public float SongReplayChance = 0.5f;
    }
}