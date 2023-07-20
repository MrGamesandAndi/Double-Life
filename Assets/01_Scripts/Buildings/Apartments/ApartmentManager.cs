using DialogueSystem;
using Population;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace Buildings.Apartments
{
    public class ApartmentManager : MonoBehaviour
    {
        [SerializeField] Transform _apartmentWindowsList;
        [SerializeField] GameObject _portery;

        [Header("Portery Settings")]
        [SerializeField] TextMeshProUGUI _doubleNumberField;
        [SerializeField] TextMeshProUGUI _mfRatioField;
        [SerializeField] TextMeshProUGUI _cityName;

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
            for (int i = 0; i < PopulationManager.Instance.DoublesList.Count; i++)
            {
                _apartmentWindowsList.GetChild(i).GetComponent<WindowManager>()._double = PopulationManager.Instance.DoublesList[i];
                _apartmentWindowsList.GetChild(i).GetComponent<ProblemDialogue>().ChangeEmote(PopulationManager.Instance.DoublesList[i].CurrentState);
                _apartmentWindowsList.GetChild(i).GetComponent<BoxCollider>().enabled = true;
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

            return $"{males}/{females}";
        }
    }
}