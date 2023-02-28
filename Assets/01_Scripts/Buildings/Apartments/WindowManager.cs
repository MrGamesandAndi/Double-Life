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

        private void OnMouseDown()
        {
            GameManager.Instance.currentLoadedDouble = _double;
            ScenesManager.Instance.LoadScene(Scenes.Apartment_Room, Scenes.Apartment_Interior);
        }
    }
}
