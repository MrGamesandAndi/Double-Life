using General;
using Localisation;
using SaveSystem;
using UnityEditor;
using UnityEngine;

namespace Buildings.TownHall
{
    public class DeleteAllData : MonoBehaviour
    {
        [SerializeField] LocalisedString _question;
        public void DeleteData()
        {
            //ModalWindow.Instance.ShowQuestion(_question.Value, () => { StartDeleteProcess(); }, () => { ModalWindow.Instance.Hide(); });
			StartDeleteProcess();
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
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}