using Needs;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General
{
    public class PopulationManager : MonoBehaviour
    {
        public static PopulationManager Instance { get; private set; } = null;
        public List<CharacterData> DoublesList { get => _doublesList; set => _doublesList = value; }
        public List<NeedsManager> DoublesAI { get => _doublesAI; set => _doublesAI = value; }

        [SerializeField] List<CharacterData> _doublesList;
        [SerializeField] List<NeedsManager> _doublesAI;
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


        private void Start()
        {
            LoadAllDoubles();
        }

        private void LoadAllDoubles()
        {
            DoublesList = FileHandler.ReturnAllFilesInFolder<CharacterData>(SaveType.Character_Data);
            DoublesList = DoublesList.OrderBy(o => o.Id).ToList();
        }

        public void AddDouble(CharacterData characterData)
        {
            DoublesList.Add(new CharacterData(
                 characterData.Name,
                 characterData.LastName,
                 characterData.Nickname,
                 characterData.RelationshipCode,
                 characterData.Color,
                 characterData.ZodiacCode,
                 characterData.SexPreferenceCode,
                 characterData.Skintone,
                 characterData.HairKey,
                 characterData.HairColor,
                 characterData.EyebrowKey,
                 characterData.EyebrowColor,
                 characterData.EyeKey,
                 characterData.EyeColor,
                 characterData.MouthKey,
                 ReturnTraits(characterData.Traits),
                 characterData.Gender,
                 characterData.CurrentState,
                 characterData.Level,
                 characterData.Experience,
                 characterData.Id,
                 new List<Vector3>(),
                 new List<Vector3>()
                 ));
        }

        private List<int> ReturnTraits(List<int> oldTraits)
        {
            List<int> newTraits = new List<int>();

            foreach (var oldTrait in oldTraits)
            {
                newTraits.Add(oldTrait);
            }

            return newTraits;
        }

        public CharacterData ReturnDouble(int id)
        {
            foreach (CharacterData result in DoublesList)
            {
                if (result.Id == id)
                {
                    return result;
                }
            }

            Debug.Log($"Could not find Double with ID {id}.");
            return new CharacterData();
        }

        public CharacterData GetRandomDouble()
        {
            var randomDoubles = (from s in DoublesList
                                where s.Id != GameManager.Instance.currentLoadedDouble.Id
                                select s).ToList();
            return randomDoubles[Random.Range(0, randomDoubles.Count)];
        }

        public NeedsManager GetAIByID(int id)
        {
            foreach (var item in DoublesAI)
            {
                if (item.characterId == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}