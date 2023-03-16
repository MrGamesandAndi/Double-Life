using General;
using SaveSystem;
using UnityEngine;
using Yarn.Unity;

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
        public static void StartNewFriendWithRandom()
        {
            CharacterData newFriend = PopulationManager.Instance.GetRandomDouble();
            AddNewRelationship(GameManager.Instance.currentLoadedDouble.Id, newFriend.Id, 4);
        }

        public static void AddNewRelationship(int initiatorID, int targetID, int amount)
        {
            PopulationManager.Instance.ReturnDouble(initiatorID).Relationships.Add(new Vector3(targetID, amount, 0));
            PopulationManager.Instance.ReturnDouble(targetID).Relationships.Add(new Vector3(initiatorID, amount, 0));
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
                    if(item.z == 1)
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
                    if (item.y != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void TalkToExistingFriend(CharacterData character)
        {
            int index = Random.Range(0, GameManager.Instance.currentLoadedDouble.Relationships.Count);
            Vector3 relationship = GameManager.Instance.currentLoadedDouble.Relationships[index];

            if(relationship.y != 7 && relationship.y != 1)
            {
                relationship.y += Mathf.Clamp(Random.Range(0, 101), -1, 1);
                PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Id).Relationships[index] = relationship;
                PopulationManager.Instance.ReturnDouble((int)relationship.x).Relationships[index] = relationship;
            }
        }
    }
}
