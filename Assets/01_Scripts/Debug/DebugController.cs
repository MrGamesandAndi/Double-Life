using Buildings.Apartments.Rooms;
using General;
using Needs;
using Population;
using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool _showConsole;
    bool _showHelp;
    bool _showNeeds;
    string _input;
    Vector2 _scroll;

    public static DebugCommand TEST;
    public static DebugCommand ROSEBUD;
    public static DebugCommand<int> SET_MONEY;
    public static DebugCommand ALL_BUILDINGS;
    public static DebugCommand HELP;
    public static DebugCommand ALL_TREASURES;
    public static DebugCommand ALL_FOOD;
    public static DebugCommand ALL_FURNITURE;
    public static DebugCommand SHOW_NEEDS;


    public List<object> commandList;

    private void Awake()
    {
        TEST = new DebugCommand("test", "Test debug functionality.", "test", () =>
        {
            Debug.Log("Working");
        });

        ROSEBUD = new DebugCommand("rosebud", "Adds 1000 money.", "rosebud", () =>
        {
            GameManager.Instance.GainFunds(100000);
        });

        SET_MONEY = new DebugCommand<int>("set_money", "Sets the amount of money.", "set_money <money_amount>", (x) =>
        {
            GameManager.Instance.GainFunds(x);
        });

        HELP = new DebugCommand("help", "Shows a list of commands.", "help", () =>
        {
            _showHelp = true;
        });

        ALL_BUILDINGS = new DebugCommand("all_buildings", "Unlocks all buildings.", "all_buildings", () =>
        {
            AchievementManager.instance.Unlock("Unlock_TownHall");
            AchievementManager.instance.Unlock("Unlock_Furniture");
            AchievementManager.instance.Unlock("Unlock_Park");
            AchievementManager.instance.Unlock("Unlock_Pawn");
            AchievementManager.instance.Unlock("Unlock_TV");
            AchievementManager.instance.Unlock("Unlock_Supermarket");
            //AchievementManager.instance.SaveAchievementState();
        });

        ALL_TREASURES = new DebugCommand("all_treasures", "Unlocks all treasures.", "all_treasures", () =>
        {
            for (int i = 0; i < SaveManager.Instance.PlayerData.obtainedTreasures.Length; i++)
            {
                SaveManager.Instance.PlayerData.obtainedTreasures[i].amount = 50;
            }
        });

        ALL_FOOD = new DebugCommand("all_food", "Unlocks all food.", "all_food", () =>
        {
            SaveManager.Instance.FoodData.Clear();

            for (int i = 0; i < BodyPartsCollection.Instance.food.Count; i++)
            {
                PurchasedItem purchasedItem = new PurchasedItem(BodyPartsCollection.Instance.food[i].foodName, 50);
                SaveManager.Instance.FoodData.Add(purchasedItem);
            }
        });

        ALL_FURNITURE = new DebugCommand("all_furniture", "Unlocks all furniture.", "all_furniture", () =>
        {
            SaveManager.Instance.FurnitureData.Clear();

            for (int i = 0; i < BodyPartsCollection.Instance.furniture.Count; i++)
            {
                PurchasedItem purchasedItem = new PurchasedItem(BodyPartsCollection.Instance.furniture[i].furnitureName, 50);
                SaveManager.Instance.FurnitureData.Add(purchasedItem);
            }
        });

        SHOW_NEEDS = new DebugCommand("show_needs", "(ONLY USE IN AN APARTMENT ROOM) Shows needs decay in a numerical way.", "show_needs", () =>
        {
            RoomManager.Instance.DEBUG_ShowNeeds(true);
        });

        commandList = new List<object>
        {
            TEST,
            ROSEBUD,
            SET_MONEY,
            HELP,
            ALL_BUILDINGS,
            ALL_TREASURES,
            ALL_FOOD,
            ALL_FURNITURE,
            SHOW_NEEDS
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _showConsole = !_showConsole;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_showConsole)
            {
                HandleInput();
                _input = "";
            }
        }
    }

    private void OnGUI()
    {
        if (!_showConsole)
        {
            return;
        }

        float y = 0f;

        if (_showHelp)
        {
            GUI.Box(new Rect(0f, y, Screen.width, 100f), "");
            Rect viewport = new Rect(0f, 0f, Screen.width - 30f, 20f * commandList.Count);
            _scroll = GUI.BeginScrollView(new Rect(0f, y + 5f, Screen.width, 90f), _scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.CommandFormat} - {command.CommandDescription}";
                Rect labelRect = new Rect(5f, 20f * i, viewport.width - 100f, 20f);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100f;
        }

        GUI.Box(new Rect(0f, y, Screen.width, 30f), "");
        GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
        _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
    }

    private void HandleInput()
    {
        string[] properties = _input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (_input.Contains(commandBase.CommandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    //Cast to this type and invoke command
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
