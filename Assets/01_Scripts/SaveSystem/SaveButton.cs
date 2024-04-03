using AudioSystem;
using General;
using Localisation;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] LocalisedString _savePrompt;
        [SerializeField] LocalisedString _saveContinuePrompt;

        public void StartSavePrompt()
        {
            ModalWindow.Instance.ShowQuestion(_savePrompt.Value, () => 
                { 
                    SaveManager.Instance.SaveAllData();
                    StartQuitPrompt();
                },() =>
                {
                    StartQuitPrompt();
                }
            );
        }
        public void StartQuitPrompt()
        {
            ModalWindow.Instance.ShowQuestion(_saveContinuePrompt.Value, () =>
            {
                ModalWindow.Instance.Hide();
            }, () =>
            {
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#endif
            });
        }
    }
}
