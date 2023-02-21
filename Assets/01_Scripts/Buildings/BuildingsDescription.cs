using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buildings
{
    public class BuildingsDescription : MonoBehaviour
    {
        public static BuildingsDescription Instance { get; protected set; }

        [SerializeField] TextMeshProUGUI _descriptionSpace;
        [SerializeField] Image _titleSpace;
        [SerializeField] Button _enterButton;

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

        public void ShowDescription(string description, Sprite _titleImage, UnityAction buttonEvent)
        {
            _descriptionSpace.text = description;
            _titleSpace.sprite = _titleImage;
            _enterButton.onClick.AddListener(buttonEvent);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}