using General;
using Needs;
using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SaveSystem.CharacterData;

namespace Population
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
                 new List<FurnitureData>(),
                 new List<RelationshipData>()
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

        public CharacterData GetRandomDouble(bool includeCurrentLoadedDouble)
        {
            List<CharacterData> randomDoubles = new List<CharacterData>();

            if (includeCurrentLoadedDouble)
            {
                randomDoubles = (from s in DoublesList
                                     select s).ToList();
            }
            else
            {
                randomDoubles = (from s in DoublesList
                                     where s.Id != GameManager.Instance.currentLoadedDouble.Id
                                     select s).ToList();
            }

            if(randomDoubles.Count == 1)
            {
                return randomDoubles[0];
            }
            else
            {
                return randomDoubles[Random.Range(0, randomDoubles.Count)];
            }
        }

        public NeedsManager GetAIByID(int id)
        {
            foreach (var item in DoublesAI)
            {
                if (item._characterId == id)
                {
                    return item;
                }
            }

            return null;
        }

        public void RemoveDouble(int id)
        {
            DoublesList.RemoveAll(x => x.Id == id);
        }

        public List<CharacterData> GetAllNoFamilyFromRelationshipData(List<RelationshipData> relationships)
        {
            List<CharacterData> result = new List<CharacterData>();

            foreach (var item in relationships)
            {
                if(ReturnDouble(item.targetId).RelationshipCode == "cc_rel_1" || ReturnDouble(item.targetId).RelationshipCode == "cc_rel_6")
                {
                    result.Add(ReturnDouble(item.targetId));
                }
            }

            return result;
        }

        public int GetRandomFriendId(int id)
        {
            List<RelationshipData> relationships = new List<RelationshipData>();
            relationships = ReturnDouble(id).Relationships;
            return relationships[Random.Range(0, relationships.Count)].targetId;
        }

        public List<CharacterData> GetAllUnknownDoubles(List<RelationshipData> relationships)
        {
            List<CharacterData> result = new List<CharacterData>();

            if (relationships.Count > 0)
            {
                foreach (var character in DoublesList)
                {
                    if (relationships.Any(relation => character.Id != relation.targetId && character.Id != GameManager.Instance.currentLoadedDouble.Id))
                    {
                        result.Add(character);
                    }
                }

            }
            else
            {
                result.Add(GetRandomDouble(false));
            }

            return result;
        }
    }
}