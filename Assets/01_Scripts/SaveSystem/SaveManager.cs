using General;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; } = null;
        public List<PurchasedFoods> FoodData { get => _foodData; set => _foodData = value; }
        public List<PurchasedFurniture> FurnitureData { get => _furnitureData; set => _furnitureData = value; }

        public PlayerData PlayerData { get => _playerData; set => _playerData = value; }
        public List<CharacterData> DoublesData { get => _doublesData; set => _doublesData = value; }

        [Header("Food Data")]
        [SerializeField] List<PurchasedFoods> _foodData; 

        [Header("Furniture Data")]
        [SerializeField] List<PurchasedFurniture> _furnitureData;

        [Header("Player Data")]
        [SerializeField] PlayerData _playerData;

        [Header("Character Data")]
        [SerializeField] List<CharacterData> _doublesData;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadFoodData();
            LoadFurnitureData();
            LoadPlayerData();
            LoadCharacterData();
        }

        public void LoadFoodData()
        {
            FoodData = FileHandler.ReadListFromJSON<PurchasedFoods>("Food_Data");

            if (FoodData == null)
            {
                DeleteFoodData();
                FoodData = FileHandler.ReadListFromJSON<PurchasedFoods>("Food_Data");
            }
        }

        public void SaveFoodData()
        {
            FileHandler.SaveToJSON(FoodData, "Food_Data");
        }

        public void LoadPlayerData()
        {
            PlayerData = FileHandler.ReadFromJSON<PlayerData>("Player_Data");

            if (PlayerData == null)
            {
                PlayerData = new PlayerData();
            }
        }

        public void SavePlayerData()
        {
            PlayerData.isOnTutorial = false;
            FileHandler.SaveToJSON(PlayerData, "Player_Data");
            AchievementManager.instance.SaveAchievementState();
        }

        public void LoadFurnitureData()
        {
            FurnitureData = FileHandler.ReadListFromJSON<PurchasedFurniture>("Furniture_Data");

            if (FurnitureData == null)
            {
                DeleteFurnitureData();
                FurnitureData = FileHandler.ReadListFromJSON<PurchasedFurniture>("Furniture_Data");
            }
        }

        public void SaveFurnitureData()
        {
            FileHandler.SaveToJSON(FurnitureData, "Furniture_Data");
        }

        public void LoadCharacterData()
        {
            DoublesData = FileHandler.ReturnAllFilesInFolder<CharacterData>(SaveType.Character_Data);
        }

        public void SaveAllData()
        {
            SaveFoodData();
            SavePlayerData();
            SaveFurnitureData();
            SaveAllCharacterData();
            AchievementManager.instance.SaveAchievementState();
        }

        public void SaveAllCharacterData()
        {
            foreach (var character in PopulationManager.Instance.DoublesList)
            {
                FileHandler.SaveToJSON(character, character.Name + character.LastName, SaveType.Character_Data);
            }
        }

        public void DeleteAllCharacterData()
        {
            DoublesData = FileHandler.ReturnAllFilesInFolder<CharacterData>(SaveType.Character_Data);

            foreach (var character in DoublesData)
            {
                FileHandler.DeleteFileFromFolder(character.Name + character.LastName, SaveType.Character_Data);
                PopulationManager.Instance.DoublesList = FileHandler.ReturnAllFilesInFolder<CharacterData>(SaveType.Character_Data);
            }
        }

        public void DeletePlayerData()
        {
            FileHandler.DeleteFileFromFolder("Player_Data", SaveType.Player_Data);
        }

        public void DeleteFoodData()
        {
            FoodData = new List<PurchasedFoods>();
            SaveFoodData();
        }

        public void DeleteFurnitureData()
        {
            FurnitureData = new List<PurchasedFurniture>();
            SaveFurnitureData();
        }
    }
}
