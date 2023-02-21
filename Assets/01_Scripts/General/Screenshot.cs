using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace CharacterCreator
{
    public class Screenshot : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] Camera _camera;
        [SerializeField] string _pathFolder;
        [SerializeField] List<GameObject> _sceneObjects;
        [SerializeField] List<BaseScriptableObject> _dataObjects;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        [ContextMenu("Screenshot")]
        private void ProcessScreenshot()
        {
            StartCoroutine(TakeScreenshot());
        }

        private IEnumerator TakeScreenshot()
        {
            for (int i = 0; i < _sceneObjects.Count; i++)
            {
                GameObject obj = _sceneObjects[i];
                BaseScriptableObject data = _dataObjects[i];
                obj.gameObject.SetActive(true);
                yield return null;
                TakeShot($"{Application.dataPath}/{_pathFolder}/{data.name}_Icon.png");
                yield return null;
                obj.gameObject.SetActive(false);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{_pathFolder}/{data.name}_Icon.png");

                if (sprite != null)
                {
                    data.Icon = sprite;
                    EditorUtility.SetDirty(data);
                }

                yield return null;
            }
        }

        private void TakeShot(string fullPath)
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>();
            }

            RenderTexture rt = new RenderTexture(256, 256, 24);
            _camera.targetTexture = rt;
            Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            _camera.targetTexture = null;
            RenderTexture.active = null;

            if (Application.isEditor)
            {
                DestroyImmediate(rt);
            }
            else
            {
                Destroy(rt);
            }

            byte[] bytes = screenshot.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);

            #if UNITY_EDITOR
                        AssetDatabase.Refresh();
            #endif
        }
#endif
    }
}