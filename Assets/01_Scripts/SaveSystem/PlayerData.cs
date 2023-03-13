using Localisation;
using System;

namespace SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public string cityName;
        public float currency;
        public Language language;
        public float musicVolume;
        public int resolutionX;
        public int resolutionY;
        public bool isOnTutorial;
        public Treasures[] obtainedTreasures;
        public int lastUsedCharacterID;

        public PlayerData()
        {
            cityName = "";
            currency = 5000f;
            language = Language.English;
            musicVolume = 1f;
            resolutionX = 1920;
            resolutionY = 1080;
            isOnTutorial = true;
            obtainedTreasures = new Treasures[0];
            lastUsedCharacterID = 100;
        }
    }

    [Serializable]
    public struct Treasures
    {
        public int id;
        public int amount;
    }
}