using SaveSystem;
using ShopSystem;
using UnityEngine;

public class GameManager : MonoBehaviour, IPurchaser
{
    public static GameManager Instance { get; private set; } = null;   

    [SerializeField] int _targetFrameRate = 60;
    public CharacterData currentLoadedDouble;

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
        PreparePresentation();      
    }

    private void PreparePresentation()
    {
        Application.targetFrameRate = _targetFrameRate;
        Screen.SetResolution(SaveManager.Instance.PlayerData.resolutionX, SaveManager.Instance.PlayerData.resolutionY, Screen.fullScreen);
    }

    public float GetCurrentFunds()
    {
        return SaveManager.Instance.PlayerData.currency;
    }

    public bool SpendFundsForFood(FoodItem food)
    {
        bool hasFind = false;

        if (SaveManager.Instance.PlayerData.currency >= food.cost)
        {
            for (int i = 0; i < SaveManager.Instance.FoodData.Count; i++)
            {
                if (SaveManager.Instance.FoodData[i].foodName.key == food.foodName.key)
                {
                    SaveManager.Instance.FoodData[i].amount++;
                    hasFind = true;
                    break;
                }
            }

            if (!hasFind)
            {
                SaveManager.Instance.FoodData.Add(new PurchasedFoods(food.foodName, 1));
            }

            SpendFunds(food.cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpendFunds(int amount)
    {
        SaveManager.Instance.PlayerData.currency -= amount;
    }

    public bool SpendFundsForFurniture(FurnitureItem furniture)
    {
        bool hasFind = false;

        if (SaveManager.Instance.PlayerData.currency >= furniture.cost)
        {
            for (int i = 0; i < SaveManager.Instance.FurnitureData.Count; i++)
            {
                if (SaveManager.Instance.FurnitureData[i].furnitureName.key == furniture.furnitureName.key)
                {
                    SaveManager.Instance.FurnitureData[i].amount++;
                    hasFind = true;
                    break;
                }
            }

            if (!hasFind)
            {
                SaveManager.Instance.FurnitureData.Add(new PurchasedFurniture(furniture.furnitureName, 1));
            }

            SpendFunds(furniture.cost);
            return true;
        }
        else
        {
            return false;
        }

    }

    public void GainFunds(int amount)
    {
        SaveManager.Instance.PlayerData.currency += amount;
        AchievementManager.instance.AddAchievementProgress("Unlock_Furniture", amount);
    } 
}