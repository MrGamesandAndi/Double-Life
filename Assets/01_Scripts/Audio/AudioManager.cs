using SaveSystem;
using System.Collections;
using UnityEngine;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        [SerializeField] AudioClip _backgroundMusic;

        AudioSource _musicSource;
        AudioSource _otherMusicSource;
        AudioSource _sfxSource;
        bool _firstMusicSourceIsPlaying;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Trying to create second AudioManager on {gameObject.name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            ConfigureAudioManager();
        }

        private void Start()
        {
            if (_backgroundMusic != null)
            {
                PlayMusic();
                SetMusicVolume(SaveManager.Instance.PlayerData.musicVolume);
                SetSfxVolume(SaveManager.Instance.PlayerData.musicVolume);
            }
        }

        private void ConfigureAudioManager()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _otherMusicSource = gameObject.AddComponent<AudioSource>();
            _sfxSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _otherMusicSource.loop = true;
        }

        public void PlayMusic(AudioClip musicClip)
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource : _otherMusicSource;
            activeSource.clip = musicClip;
            activeSource.Play();
        }

        public void PlayMusic()
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource : _otherMusicSource;
            activeSource.clip = _backgroundMusic;
            activeSource.Play();
        }

        public void StopMusic()
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource : _otherMusicSource;
            activeSource.Stop();
        }

        public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1f)
        {
            AudioSource activeSource;

            if (_firstMusicSourceIsPlaying)
            {
                activeSource = _musicSource;
            }
            else
            {
                activeSource = _otherMusicSource;
            }

            StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
        }

        public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f)
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource : _otherMusicSource;
            AudioSource newSource = (_firstMusicSourceIsPlaying) ? _otherMusicSource : _musicSource;
            _firstMusicSourceIsPlaying = !_firstMusicSourceIsPlaying;
            newSource.clip = musicClip;
            newSource.Play();
            StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
        }

        private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
        {
            if (!activeSource.isPlaying)
            {
                activeSource.Play();
            }

            float fade = 0f;

            for (fade = 0f; fade < transitionTime; fade += Time.deltaTime)
            {
                activeSource.volume = (1 - (fade / transitionTime));
                yield return null;
            }

            activeSource.Stop();
            activeSource.clip = newClip;
            activeSource.Play();

            for (fade = 0f; fade < transitionTime; fade += Time.deltaTime)
            {
                activeSource.volume = fade / transitionTime;
                yield return null;
            }
        }

        private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
        {
            float fade = 0f;

            for (fade = 0f; fade <= transitionTime; fade += Time.deltaTime)
            {
                original.volume = (1 - (fade / transitionTime));
                newSource.volume = fade / transitionTime;
                yield return null;
            }

            original.Stop();
        }

        public void PlaySfx(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }

        public void PlaySfx(AudioClip clip, float volume)
        {
            _sfxSource.PlayOneShot(clip, volume);
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = volume;
            _otherMusicSource.volume = volume;
        }

        public void SetSfxVolume(float volume)
        {
            _sfxSource.volume = volume;
        }

        public bool CheckIfMusicIsPlaying()
        {
            if (!_musicSource.isPlaying || !_otherMusicSource.isPlaying)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfSfxIsPlaying(AudioClip sfx)
        {
            if (_sfxSource == sfx)
            {
                if (!_sfxSource.isPlaying)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}