using AudioSystem;
using Buildings.Apartments.Rooms;
using Localisation;
using SaveSystem;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public static Entity Instance { get; private set; }

    [SerializeField] Image _expBar;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] LevelConfig _levelConfig;
    [SerializeField] ParticleSystem _spaLevelUpParticle;
    [SerializeField] ParticleSystem _engLevelUpParticle;
    [SerializeField] AudioClip _levelUpSFX;

    int _level;
    int _exp;
    int _requireExp;

    private void Start()
    {
        Instance = this;
        CalculateRequiredExp();
    }

    public void IncreaseExp(int value)
    {
        _exp += value;
        UpdateUI();

        if (_exp >= _requireExp)
        {
            while (_exp >= _requireExp)
            {
                _exp -= _requireExp;
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        _level++;
        CalculateRequiredExp();
        PlayLevelUpAnimation();
        PlayLevelUpParticleEffect();
        AudioManager.Instance.PlaySfx(_levelUpSFX);
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

    public void CalculateRequiredExp()
    {
        _requireExp = _levelConfig.GetRequiredExp(_level);
        UpdateUI();
    }

    private void UpdateUI()
    {
        LeanTween.value(_expBar.fillAmount, (float)_exp / (float)_requireExp, 1).setOnUpdate((val) => { _expBar.fillAmount = val; });
        _levelText.text = _level.ToString();
    }
}
