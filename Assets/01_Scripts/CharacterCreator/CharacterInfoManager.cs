using General;
using Localisation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class CharacterInfoManager : MonoBehaviour
    {
        [SerializeField] TMP_InputField _nameInputField;
        [SerializeField] TMP_InputField _lastNameInputField;
        [SerializeField] TMP_InputField _nicknameInputField;
        [SerializeField] Button _relationshipInputField;
        [SerializeField] Button _zodiacInputField;
        [SerializeField] Button _sexPreferenceInputField;
        [SerializeField] Button _genderImage; 
        [SerializeField] HSVPicker.ColorPicker _picker;

        [SerializeField] List<Sprite> _genderImages;

        private void Start()
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            GetNameValue();
            GetLastNameValue();
            GetNicknameValue();
            GetRelationshipValue();
            GetZodiacValue();
            GetSexPreferenceValue();
            GetColorValue();

            switch (PlayerPrefs.GetInt("CC_State"))
            {
                case 0:
                    break;
                case 1:
                    _nameInputField.interactable = false;
                    _lastNameInputField.interactable = false;
                    _relationshipInputField.interactable = false;
                    _zodiacInputField.interactable = false;
                    _genderImage.interactable = false;
                    PlayerPrefs.SetInt("CC_State", 0);
                    break;
                case 2:
                    _relationshipInputField.interactable = false;
                    PlayerPrefs.SetInt("CC_State", 0);
                    break;
            }
        }

        private void GetNameValue()
        {
            _nameInputField.text = HumanController.Instance.Name;
        }

        public void SetNameValue()
        {
            HumanController.Instance.Name = _nameInputField.text;
        }

        private void GetLastNameValue()
        {
            _lastNameInputField.text = HumanController.Instance.LastName;
        }

        public void SetLastNameValue()
        {
            HumanController.Instance.LastName = _lastNameInputField.text;
        }

        private void GetNicknameValue()
        {
            _nicknameInputField.text = HumanController.Instance.Nickname;
        }

        public void SetNicknameValue()
        {
            HumanController.Instance.Nickname = _nicknameInputField.text;
        }

        private void GetRelationshipValue()
        {
            if (_relationshipInputField != null)
            {
                _relationshipInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.RelationshipCode);
            }
        }

        public void SetRelationshipValue(TextLocaliserUI text)
        {
            HumanController.Instance.RelationshipCode = text.ReturnLocalisationKey();
            _relationshipInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.RelationshipCode);
        }

        private void GetZodiacValue()
        {
            _zodiacInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.ZodiacCode);
        }

        public void SetZodiacValue(TextLocaliserUI text)
        {
            HumanController.Instance.ZodiacCode = text.ReturnLocalisationKey();
            _zodiacInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.ZodiacCode);
        }

        private void GetSexPreferenceValue()
        {
            _sexPreferenceInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.SexPreferenceCode);
        }

        public void SetSexPreferenceValue(TextLocaliserUI text)
        {
            HumanController.Instance.SexPreferenceCode = text.ReturnLocalisationKey();
            _sexPreferenceInputField.GetComponentInChildren<TextLocaliserUI>().UpdateText(HumanController.Instance.SexPreferenceCode);
        }
        private void GetColorValue()
        {
            _picker.CurrentColor = HumanController.Instance.Color;
            HumanController.Instance.SetColor(BodyTypes.Shirt, _picker.CurrentColor);
        }

        public void SetColorValue()
        {
            HumanController.Instance.SetColor(BodyTypes.Shirt, _picker.CurrentColor);
        }

        public void SetGender(int genderCode)
        {
            if (genderCode == (int)GenderType.Male)
            {
                HumanController.Instance.Gender = (int)GenderType.Male;
            }
            else
            {
                HumanController.Instance.Gender = (int)GenderType.Female;
            }

            _genderImage.GetComponentInChildren<Image>().sprite = _genderImages[genderCode];
        }
    }
}