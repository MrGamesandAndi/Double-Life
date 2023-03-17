using General;
using Needs;
using SaveSystem;
using System.Collections.Generic;
using TraitSystem;
using UnityEngine;
using Yarn.Unity;
using static SaveSystem.CharacterData;

namespace Relationships
{
    public class RelationshipSystem : MonoBehaviour
    {
        public static RelationshipSystem Instance { get; protected set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [YarnCommand("give_new_friend")]
        public void StartNewFriendWithRandom()
        {
            CharacterData newFriend = PopulationManager.Instance.GetRandomDouble();
            AddNewRelationship(GameManager.Instance.currentLoadedDouble.Id, newFriend.Id, 4);
        }

        private void AddNewRelationship(int initiatorID, int targetID, int amount)
        {
            PopulationManager.Instance.ReturnDouble(initiatorID).Relationships.Add(new RelationshipData(targetID, amount, false));
            PopulationManager.Instance.ReturnDouble(targetID).Relationships.Add(new RelationshipData(initiatorID, amount, false));
        }

        [YarnCommand("break_up")]
        public void BreakUp()
        {
            
        }

        private int GetRelationshipIndex(CharacterData initiator, int targetId)
        {
            for (int i = 0; i < initiator.Relationships.Count; i++)
            {
                if(initiator.Relationships[i].targetId == targetId)
                {
                    return i;
                }
            }

            return 0;
        }

        public bool CheckIfLoveInterestExists(CharacterData character)
        {
            if (character.Relationships.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var item in character.Relationships)
                {
                    if (item.isLove)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckIfDoubleHasRelationships(CharacterData character)
        {
            if (character.Relationships.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var item in character.Relationships)
                {
                    if (item.relationshipLevel != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void TalkToRandomExistingFriend()
        {
            int index = Random.Range(0, GameManager.Instance.currentLoadedDouble.Relationships.Count);
            RelationshipData relationship = GameManager.Instance.currentLoadedDouble.Relationships[index];

            if (relationship.relationshipLevel != 7 && relationship.relationshipLevel != 1)
            {
                relationship.relationshipLevel += Mathf.Clamp(Random.Range(0, 101), -1, 1);
                PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Id).Relationships[index] = relationship;
                PopulationManager.Instance.ReturnDouble(GetLoveInterest(GameManager.Instance.currentLoadedDouble).Id).Relationships[index] = relationship;
            }
        }

        private CharacterData GetLoveInterest(CharacterData target)
        {
            foreach (var item in target.Relationships)
            {
                if (item.isLove)
                {
                    return PopulationManager.Instance.ReturnDouble(item.targetId);
                }
            }

            return new CharacterData();
        }


        [YarnCommand("go_to_date")]
        public void GoToDate()
        {
           
        }

        [YarnCommand("reset_need")]
        private void ResetNeed(int needId)
        {
            PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted((NeedType)needId);
        }

        [YarnCommand("confess_love")]
        public void TryConfessLove()
        {
            CharacterData loveInterest = PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Relationships[Random.Range(0, GameManager.Instance.currentLoadedDouble.Relationships.Count)].targetId);

            if (CheckIfLoveInterestExists(loveInterest))
            {
                return;
            }

            float chance = 5f + CheckForTraitCompatibility(loveInterest) + CheckForZodiacCompatibility(loveInterest);

            if (chance >= Random.Range(0f, 10f))
            {
                Debug.Log(chance);
            }
            else
            {
                Debug.Log(chance);
                PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).GetNeed(NeedType.HaveDepression).SetNeed();
            }


        }

        private float CheckForTraitCompatibility(CharacterData loveInterest)
        {
            float score = 0f;
            List<Trait> currentTraits = BodyPartsCollection.Instance.ReturnTraitsFromCharacterData(GameManager.Instance.currentLoadedDouble.Traits);
            List<Trait> loveTraits = BodyPartsCollection.Instance.ReturnTraitsFromCharacterData(loveInterest.Traits);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(currentTraits[i] == loveTraits[j])
                    {
                        score += 0.6f;
                        break;
                    }

                    if (currentTraits[i].opossiteTrait == loveTraits[j])
                    {
                        score -= 0.6f;
                        break;
                    }
                }
            }

            return score;
        }

        private float CheckForZodiacCompatibility(CharacterData loveInterest)
        {
            foreach (var zodiac in BodyPartsCollection.Instance.zodiac)
            {
                foreach (var compatibleSign in zodiac.compatibleSigns)
                {
                    if (loveInterest.ZodiacCode == compatibleSign.id)
                    {
                        return 2.5f;
                    }
                }
            }

            return 0f;
        }
    }
}
