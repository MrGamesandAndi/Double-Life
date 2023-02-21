using Localisation;
using System;

namespace SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public string cityName = "";
        public float currency = 5000f;
        public Language language = Language.English;
        public float musicVolume = 1f;
        public int resolutionX = 1920;
        public int resolutionY = 1080;
        public bool isOnTutorial = true;
    }
}