using General;
using SaveSystem;
using SceneManagement;
using UnityEngine;

namespace Apartments
{
    public class WindowManager : MonoBehaviour
    {
        public CharacterData _double;
        public DoubleState state;
        public GameObject emoteWindow;

        private void OnMouseDown()
        {
            GameManager.Instance.currentLoadedDouble = _double;
            GameManager.Instance.currentState = state;
            ScenesManager.Instance.LoadScene(Scenes.Apartment_Room, Scenes.Apartment_Interior);
        }

        public void ManageEmoteWindow(bool hasToShow)
        {
            emoteWindow.SetActive(hasToShow);
        }
    }
}
