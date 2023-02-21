using DialogueSystem;
using General;
using SaveSystem;
using SmartInteractions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Apartments
{
    public class ApartmentManager : MonoBehaviour
    {
        public List<WindowManager> _apartmentWindowsList;

        [SerializeField] List<Transform> _floors;
        [SerializeField] GameObject _portery;

        [Header("Portery Settings")]
        [SerializeField] TextMeshProUGUI _doubleNumberField;
        [SerializeField] TextMeshProUGUI _mfRatioField;
        [SerializeField] TextMeshProUGUI _cityName;

        private void Awake()
        {
            for (int i = 0; i < _floors.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    WindowManager window = _floors[i].GetChild(j).GetComponent<WindowManager>();
                    _apartmentWindowsList.Add(window);
                }
            }
        }

        private void Start()
        {
            PreparePortery();
            PrepareApartment();
            _portery.SetActive(false);
        }

        private void OnMouseDown()
        {
            _portery.SetActive(true);
        }

        public void PrepareApartment()
        {
            for (int i = 0; i < GenerateAI.Instance.transform.childCount; i++)
            {
                _apartmentWindowsList[i].GetComponent<WindowManager>()._double = PopulationManager.Instance.DoublesList[i];
                _apartmentWindowsList[i].GetComponent<WindowManager>().ManageEmoteWindow(true);
                _apartmentWindowsList[i].GetComponent<BoxCollider>().enabled = true;
            }
        }

        private void PreparePortery()
        {
            _doubleNumberField.text = PopulationManager.Instance.DoublesList.Count.ToString();
            _mfRatioField.text = CalculateMFRatio();
            _cityName.text = SaveManager.Instance.PlayerData.cityName;
        }

        public string CalculateMFRatio()
        {
            int males = 0;
            int females = 0;
            string result = "";

            foreach (var person in PopulationManager.Instance.DoublesList)
            {
                if (person.Gender == 0)
                {
                    males++;
                }
                else
                {
                    females++;
                }
            }

            result = $"{males}/{females}";
            return result;
        }
    }
}