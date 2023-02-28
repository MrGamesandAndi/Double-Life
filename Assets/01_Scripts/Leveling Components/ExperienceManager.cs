using General;
using Localisation;
using SaveSystem;
using UnityEngine;

namespace LevelingSystem
{
    public class ExperienceManager : MonoBehaviour
    {
        public static ExperienceManager Instance { get; protected set; }
        public LevelSystem LevelSystem { get => _levelSystem; }

        [SerializeField] LevelWindow _levelWindow;
        [SerializeField] ParticleSystem _spaLevelUpParticle;
        [SerializeField] ParticleSystem _engLevelUpParticle;

        LevelSystem _levelSystem;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _levelSystem = new LevelSystem();
            _levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
            _levelWindow.SetLevelSystem();
        }

        private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
        {
            PlayLevelUpAnimation();
            PlayLevelUpParticleEffect();
        }

        private void PlayLevelUpAnimation()
        {
            HumanController.Instance.PlayAnimation(HumanAnimationStates.Level_Up.ToString());
        }
        
        private void PlayLevelUpParticleEffect()
        {
            switch (SaveManager.Instance.PlayerData.language)
            {
                case Language.Spanish:
                    _spaLevelUpParticle.Play();
                    break;
                case Language.English:
                    _engLevelUpParticle.Play();
                    break;
                default:
                    break;
            }
        }
    }
}