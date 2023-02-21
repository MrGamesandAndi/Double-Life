using TMPro;
using UnityEngine;

namespace Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocaliserUI : MonoBehaviour
    {
        TextMeshProUGUI _textField; //Text field that will contain the text.
        [SerializeField] LocalisedString _localisedString; //Localised text.

        private void Awake()
        {
            _textField = GetComponent<TextMeshProUGUI>();
            UpdateText(_localisedString);
        }

        public void UpdateText(LocalisedString localisedString)
        {
            _textField.text = localisedString.Value;
        }

        public string ReturnLocalisationKey()
        {
            return _localisedString.key;
        }

        public string ReturnLocalisationValue()
        {
            return _localisedString.Value;
        }
    }
}

