using Needs;
using SaveSystem;
using UnityEngine;

namespace General
{
    public class GenerateAI : MonoBehaviour
    {
        public static GenerateAI Instance { get; private set; } = null;
        [SerializeField] GameObject _aiPrefab;

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

        public void CreateAI()
        {
            foreach (CharacterData character in PopulationManager.Instance.DoublesList)
            {
                AddIndividualAI(character);
            }
        }

        public void Start()
        {
            CreateAI();
        }

        public void AddIndividualAI(CharacterData character)
        {
            GameObject ai = _aiPrefab;
            ai.name = $"{character.Name}{character.LastName}";
            ai.GetComponent<NeedsManager>().LinkCharacterData(character.Id);
            ai.GetComponent<NeedsManager>().SetMultipliers();
            PopulationManager.Instance.DoublesAI.Add(Instantiate(ai, transform).GetComponent<NeedsManager>());
        }

        public void RemoveIndividualAI(CharacterData character)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var data = transform.GetChild(i).gameObject;

                if(data.name == character.Name + character.LastName)
                {
                    PopulationManager.Instance.DoublesAI.RemoveAt(i);
                    Destroy(data);
                }
            }
        }
    }
}
