using Buildings.ShopSystem;
using General;
using Needs;
using Population;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
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
            ResetNeed((int)NeedType.MakeFriend);
            GainTreasure();
        }

        public void GainTreasure()
        {
            //RoomManager.Instance.DialogueRunner.StartDialogue("Thanks");
            Treasure gainedTreasure = BodyPartsCollection.Instance.ReturnRandomTreasure(TreasureRarity.UltraRare);
            GameManager.Instance.GainTreasure(gainedTreasure.id, 1);
        }

        public void DeleteRelationship(int targetID)
        {
            foreach (var item in PopulationManager.Instance.DoublesList)
            {
                if(item.Id != targetID)
                {
                    for (int i = 0; i < item.Relationships.Count; i++)
                    {
                        if (item.Relationships[i].targetId == targetID)
                        {
                            SetLoveLevel(item.Id, targetID, false);
                            item.Relationships.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        public void AddNewRelationship(int initiatorID, int targetID, int amount, bool isLove = false)
        {
            PopulationManager.Instance.ReturnDouble(initiatorID).Relationships.Add(new RelationshipData(targetID, amount, isLove));
            PopulationManager.Instance.ReturnDouble(targetID).Relationships.Add(new RelationshipData(initiatorID, amount, isLove));
        }

        [YarnCommand("break_up")]
        public void BreakUp()
        {
            SetLoveLevel(GameManager.Instance.currentLoadedDouble.Id, GetLoveInterestId(), false);
            ResetNeed((int)NeedType.BreakUp);
        }

        private void SetRelationshipLevel(int initiatorID, int targetID, int amount)
        {
            PopulationManager.Instance.ReturnDouble(initiatorID).Relationships.First(i => i.targetId == targetID).SetNewRelationshipValues(amount);
            PopulationManager.Instance.ReturnDouble(targetID).Relationships.First(i => i.targetId == initiatorID).SetNewRelationshipValues(amount);
        }

        private void SetLoveLevel(int initiatorID, int targetID, bool isLove)
        {
            PopulationManager.Instance.ReturnDouble(initiatorID).Relationships.First(i => i.targetId == targetID).SetLoveStatus(isLove);
            PopulationManager.Instance.ReturnDouble(targetID).Relationships.First(i => i.targetId == initiatorID).SetLoveStatus(isLove);
        }

        private int GetLoveInterestId()
        {
            return PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Id).Relationships.First(i => i.isLove).targetId;
        }

        public bool CheckIfLoveInterestExists(int id)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(id);

            if (character.Relationships.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var item in character.Relationships)
                {
                    if(id == 1)
                    {
                        if (PopulationManager.Instance.ReturnDouble(item.targetId).RelationshipCode == "cc_rel_1" ||
                        PopulationManager.Instance.ReturnDouble(item.targetId).RelationshipCode == "cc_rel_6")
                        {
                            if (item.isLove)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (item.isLove)
                        {
                            return true;
                        }
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
            int randomChance = Random.Range(0, 11);

            if(randomChance >= 5)
            {
                SetRelationshipLevel(GameManager.Instance.currentLoadedDouble.Id, GameManager.Instance.currentLoadedDouble.Relationships[index].targetId, 1);
            }
            else
            {
                SetRelationshipLevel(GameManager.Instance.currentLoadedDouble.Id, GameManager.Instance.currentLoadedDouble.Relationships[index].targetId, -1);
            }
        }

        [YarnCommand("go_to_date")]
        public void GoToDate()
        {
            int randomChance = Random.Range(0, 11);

            if (randomChance >= 5)
            {
                SetRelationshipLevel(GameManager.Instance.currentLoadedDouble.Id, GetLoveInterestId(), 1);
                GainTreasure();
            }
            else
            {
                SetRelationshipLevel(GameManager.Instance.currentLoadedDouble.Id, GetLoveInterestId(), -1);
            }

            ResetNeed((int)NeedType.HaveDate);
        }

        [YarnCommand("reset_need")]
        public void ResetNeed(int needId)
        {
            PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted((NeedType)needId);
        }

        [YarnCommand("confess_love")]
        public void TryConfessLove()
        {
            List<CharacterData> result = new List<CharacterData>();

            result = GameManager.Instance.currentLoadedDouble.Id == 1
                ? PopulationManager.Instance.GetAllNoFamilyFromRelationshipData(GameManager.Instance.currentLoadedDouble.Relationships)
                : PopulationManager.Instance.DoublesList;


            if (result.Count > 0)
            {
                CharacterData loveInterest = result[Random.Range(0, result.Count)];
                float chance = CheckForTraitCompatibility(loveInterest) + CheckForZodiacCompatibility(loveInterest);

                if (chance >= 5f)
                {
                    SetLoveLevel(GameManager.Instance.currentLoadedDouble.Id, loveInterest.Id, true);
                    GainTreasure();
                }
                else
                {
                    PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).GetNeed(NeedType.HaveDepression).SetNeed();
                }
            }

            ResetNeed((int)NeedType.ConfessLove);
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
                        score += 2.5f;
                        break;
                    }

                    if (currentTraits[i].opossiteTrait == loveTraits[j])
                    {
                        score -= 2.5f;
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
                        return 5f;
                    }
                }
            }

            return 0f;
        }
    }
}
