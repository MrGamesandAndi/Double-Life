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
        public int lastUsedID;

        public PlayerData()
        {
            cityName = "";
            currency = 5000f;
            language = Language.Spanish;
            musicVolume = 1f;
            resolutionX = 1920;
            resolutionY = 1080;
            isOnTutorial = true;
            obtainedTreasures = new Treasures[11];
            lastUsedID = 1;

            for (int i = 0; i < obtainedTreasures.Length; i++)
            {
                obtainedTreasures[i].id = i;
            }
        }
    }

    [Serializable]
    public struct Treasures
    {
        public int id;
        public int amount;
    }
}