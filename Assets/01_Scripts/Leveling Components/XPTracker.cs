using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LevelingSystem
{
    public class XPTracker : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _currentLevelText;
        [SerializeField] ProgressBar _xpBar;
        [SerializeField] BaseXPTranslation _xpTranslationType;
        [SerializeField] LocalisedString _levelAcronym;
        [SerializeField] UnityEvent OnLevelChanged = new UnityEvent();

        BaseXPTranslation _xpTranslation;
        int _previousLevel = 0;
        int _amount = 0;

        private void Awake()
        {
            _xpTranslation = Instantiate(_xpTranslationType);
        }

        private void Start()
        {
            _currentLevelText.text = $"{_levelAcronym.Value}{_xpTranslation.CurrentLevel}";
            OnLevelChanged.Invoke();
        }

        public void AddXP(int amount)
        {
            _previousLevel = _xpTranslation.CurrentLevel;
            _amount = amount;
            _xpTranslation.AddXP(amount);
            RefreshDisplays();
        }

        public void SetLevel(int level)
        {
            _previousLevel = _xpTranslation.CurrentLevel;
            _xpTranslation.SetLevel(level);
            RefreshDisplays();
        }

        private void RefreshDisplays()
        {
            LeanTween.value(_xpBar.Current, _xpBar.Current + _amount, 3f)
                .setOnUpdate((float val) => { _xpBar.Current = val; })
                .setOnComplete(() => { OnLevelChanged.Invoke(); });
        }

        public void OnLevelChange()
        {
            if (_previousLevel != _xpTranslation.CurrentLevel)
            {
                _currentLevelText.text = $"{_levelAcronym.Value}{_xpTranslation.CurrentLevel}";
                _xpBar.Current = 0;
            }
        }
    }
}
