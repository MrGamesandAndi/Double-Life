using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class ModalWindow : MonoBehaviour
    {
        public static ModalWindow Instance { get; private set; }

        [SerializeField] TextMeshProUGUI _textMeshPro;
        [SerializeField] Button _confirmationButton;
        [SerializeField] Button _cancelButton;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Hide();
        }

        public void ShowQuestion(string question, Action yesAction, Action noAction)
        {
            gameObject.SetActive(true);
            GetComponent<UITweener>().Show();
            _textMeshPro.text = "";
            _textMeshPro.text = question;
            _confirmationButton.onClick.AddListener(()=> {
                yesAction();
            });
            _cancelButton.onClick.AddListener(() => {
                noAction();
            });
        }

        public void Hide()
        {
            GetComponent<UITweener>().Disable();
            gameObject.SetActive(false);
        }
    }
}
