using TMPro;
using UnityEngine;

namespace LevelingSystem
{
    public class LevelWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _levelText;
        [SerializeField] LevelSystem _levelSystem;

        private void SetLevelNumber(int levelNumber)
        {
            _levelText.text = $"{levelNumber + 1}";
            HumanController.Instance.characterData.Level = levelNumber + 1;
        }

        public void SetLevelSystem()
        {
            _levelSystem = ExperienceManager.Instance.LevelSystem;
            SetLevelNumber(_levelSystem.GetLevelNumber());
            _levelSystem.OnLevelChanged += _levelSystem_OnLevelChanged;
        }

        private void _levelSystem_OnLevelChanged(object sender, System.EventArgs e)
        {
            SetLevelNumber(_levelSystem.GetLevelNumber());
        }
    }
}