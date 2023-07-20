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
            _levelText.text = $"{levelNumber}";
            HumanController.Instance.characterData.Level = levelNumber;
        }

        public void SetLevelSystem()
        {
            _levelSystem = ExperienceManager.Instance.LevelSystem;
            SetLevelNumber(_levelSystem.GetLevelNumber());
            _levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        }

        private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
        {
            SetLevelNumber(_levelSystem.GetLevelNumber() + 1);
        }
    }
}