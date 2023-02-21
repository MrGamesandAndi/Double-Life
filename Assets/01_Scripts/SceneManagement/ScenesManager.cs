using System.Collections;
using System.Collections.Generic;
using TrafficSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager Instance { get; private set; } = null;

        [SerializeField] GameObject _loadingScreen;

        List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
        float _totalSceneProgress;
        float _totalSpawnProgress;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Trying to create second ScenesManager on {gameObject.name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            SceneManager.LoadSceneAsync((int)Scenes.Title_Screen, LoadSceneMode.Additive);
        }

        public void LoadScene(Scenes sceneToLoad, Scenes previousScene)
        {
            _loadingScreen.gameObject.SetActive(true);
            _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)previousScene));
            _scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive));
            StartCoroutine(GetSceneLoadProgress());

            if (sceneToLoad == Scenes.City)
            {
                StartCoroutine(GetTotalPedestrianProgress());
            }
            else
            {
                StartCoroutine(GetGenericSceneLoadProgress());
            }
        }

        public IEnumerator GetSceneLoadProgress()
        {
            for (int i = 0; i < _scenesLoading.Count; i++)
            {
                while (!_scenesLoading[i].isDone)
                {
                    _totalSceneProgress = 0;

                    foreach (AsyncOperation operation in _scenesLoading)
                    {
                        _totalSceneProgress += operation.progress;
                    }

                    _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                    yield return null;
                }
            }
        }

        public IEnumerator GetTotalPedestrianProgress()
        {
            float totalProgress = 0;

            while (PedestrianSpawner.Instance == null || !PedestrianSpawner.Instance.IsDone)
            {
                if (PedestrianSpawner.Instance == null)
                {
                    _totalSpawnProgress = 0f;
                }
                else
                {
                    _totalSpawnProgress = Mathf.Round(PedestrianSpawner.Instance.Progress * 100f);
                }

                totalProgress = Mathf.Round((_totalSceneProgress + _totalSpawnProgress) / 2f);
                yield return null;
            }

            _loadingScreen.gameObject.SetActive(false);
        }

        public IEnumerator GetGenericSceneLoadProgress()
        {
            for (int i = 0; i < _scenesLoading.Count; i++)
            {
                while (!_scenesLoading[i].isDone)
                {
                    _totalSceneProgress = 0;

                    foreach (AsyncOperation operation in _scenesLoading)
                    {
                        _totalSceneProgress += operation.progress;
                    }

                    _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);
            _loadingScreen.gameObject.SetActive(false);
        }
    }
}