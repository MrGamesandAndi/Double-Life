using General;
using Localisation;
using SaveSystem;
using UnityEditor;
using UnityEngine;

public class DeleteAllData : MonoBehaviour
{
    [SerializeField] LocalisedString _question;
    public void DeleteData()
    {
        ModalWindow.Instance.ShowQuestion( _question.Value, () => { StartDeleteProcess(); }, () => { });
    }

    private void StartDeleteProcess()
    {
        PlayerPrefs.DeleteAll();
        SaveManager.Instance.DeleteAllCharacterData();
        SaveManager.Instance.DeletePlayerData();
        SaveManager.Instance.FoodData.Clear();
        SaveManager.Instance.FurnitureData.Clear();
        SaveManager.Instance.SaveFoodData();
        SaveManager.Instance.SaveFurnitureData();
        AchievementManager.instance.ResetAchievementState();
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
