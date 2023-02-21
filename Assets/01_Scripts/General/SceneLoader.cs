using SceneManagement;
using UnityEngine;

namespace General
{
    public class SceneLoader : MonoBehaviour
    {
        public Scenes previousScene;
        public Scenes sceneToLoad;

        public void LoadScene()
        {
            AchievementManager.instance.transform.GetChild(1).gameObject.SetActive(false);
            ScenesManager.Instance.LoadScene(sceneToLoad, previousScene);
        }
    }
}
