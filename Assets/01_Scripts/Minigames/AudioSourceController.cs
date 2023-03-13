using UnityEngine;

namespace ParkMinigame
{
    public class AudioSourceController : MonoBehaviour
    {
        [SerializeField] AudioClip _blip;
        [SerializeField] AudioClip _dead;
        [SerializeField] AudioClip _end;
        [SerializeField] AudioClip _shootGas;

        AudioSource _audioSource;

        private void OnEnable()
        {
            WormController.Move += PlayBlipAudio;
            WormController.InFlower += PlayDeadAudio;
            PlayerController.Shoot += PlayShootAudio;
        }

        private void OnDisable()
        {
            WormController.Move -= PlayBlipAudio;
            WormController.InFlower -= PlayDeadAudio;
            PlayerController.Shoot -= PlayShootAudio;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayDeadAudio()
        {
            _audioSource.PlayOneShot(_dead, 1.0f);
        }

        public void PlayEndAudio()
        {
            _audioSource.PlayOneShot(_end, 1.0f);
        }

        public void PlayBlipAudio()
        {
            _audioSource.PlayOneShot(_blip, 1.0f);
        }
        public void PlayShootAudio()
        {
            _audioSource.PlayOneShot(_shootGas, 1.0f);
        }
    }
}
