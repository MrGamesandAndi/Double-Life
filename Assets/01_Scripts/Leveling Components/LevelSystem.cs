using System;

namespace LevelingSystem
{
    public class LevelSystem
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;
        int _level;
        int _experience;
        int _experienceToNextLevel;

        public LevelSystem()
        {
            _level = HumanController.Instance.characterData.Level;
            _experience = HumanController.Instance.characterData.Experience;
            _experienceToNextLevel = 100;
        }

        public void AddExperience(int amount)
        {
            _experience += amount;

            while (_experience >= _experienceToNextLevel)
            {
                _level++;
                _experience -= _experienceToNextLevel;

                if (OnLevelChanged != null)
                {
                    OnLevelChanged(this, EventArgs.Empty);
                }
            }

            if (OnExperienceChanged != null)
            {
                OnExperienceChanged(this, EventArgs.Empty);
            }
        }

        public int GetLevelNumber()
        {
            return _level;
        }

        public float GetExperienceNormalized()
        {
            return (float)_experience / _experienceToNextLevel;
        }

        public int GetExperience()
        {
            return _experience;
        }

        public int GetExperienceToNextLevel()
        {
            return _experienceToNextLevel;
        }
    }
}
