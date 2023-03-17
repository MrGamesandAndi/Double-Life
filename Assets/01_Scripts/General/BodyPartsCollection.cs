using CharacterCreator;
using Relationships;
using SaveSystem;
using ShopSystem;
using System.Collections.Generic;
using TraitSystem;
using UnityEngine;

namespace General
{
    public class BodyPartsCollection : MonoBehaviour
    {
        public static BodyPartsCollection Instance { get; protected set; }

        private void Awake()
        {
            Instance = this;
        }

        [Header("Furniture")]
        public List<FurnitureItem> furniture;


        [Header("Hair Parts")]
        public List<BaseScriptableObject> hairs;

        [Header("Eyebrows Parts")]
        public List<BaseScriptableObject> eyebrows;

        [Header("Eyes Parts")]
        public List<BaseScriptableObject> eyes;

        [Header("Mouth Parts")]
        public List<BaseScriptableObject> mouths;

        [Header("AI")]
        public List<Trait> traits;

        [Header("Treasures")]
        public List<Treasure> treasures;

        [Header("Zodiac")]
        public List<ZodiacSign> zodiac;

        public Mesh ReturnHairMesh(string hairName)
        {
            foreach (BaseScriptableObject hair in hairs)
            {
                if (hair.name == hairName)
                {
                    return hair.Mesh;
                }
            }

            return null;
        }

        public ZodiacSign ReturnZodiacById(int id)
        {
            foreach (var item in zodiac)
            {
                if(item.id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Sprite ReturnEyebrowSprite(string eyebrowName)
        {
            foreach (BaseScriptableObject eyebrow in eyebrows)
            {
                if (eyebrow.name == eyebrowName)
                {
                    return eyebrow.Icon;
                }
            }

            return null;
        }

        public Sprite ReturnEyeSprite(string eyeName)
        {
            foreach (BaseScriptableObject eye in eyes)
            {
                if (eye.name == eyeName)
                {
                    return eye.Icon;
                }
            }

            return null;
        }

        public Sprite ReturnMouthSprite(string mouthName)
        {
            foreach (BaseScriptableObject mouth in mouths)
            {
                if (mouth.name == mouthName)
                {
                    return mouth.Icon;
                }
            }

            return null;
        }

        public List<Trait> ReturnTraitsFromCharacterData(List<int> characterTraits)
        {
            List<Trait> returnedTraits = new List<Trait>();

            for (int i = 0; i < traits.Count; i++)
            {
                for (int j = 0; j < characterTraits.Count; j++)
                {
                    if (traits[i].id == characterTraits[j])
                    {
                        returnedTraits.Add(traits[i]);
                    }
                }
            }
            
            return returnedTraits;
        }

        public FurnitureItem GetFurniture(int id)
        {
            foreach (var item in furniture)
            {
                if (item.id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Treasure ReturnTreasure(int id)
        {
            foreach (var item in treasures)
            {
                if (item.id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Treasures ReturnPlayerTreasure(int id)
        {
            foreach (var item in SaveManager.Instance.PlayerData.obtainedTreasures)
            {
                if (item.id == id)
                {
                    return item;
                }
            }

            return new Treasures();
        }

        public Treasure ReturnRandomTreasure(TreasureRarity rarity)
        {
            List<Treasure> foundTreasures = new List<Treasure>();

            foreach (var item in treasures)
            {
                if (item.rarity == rarity)
                {
                    foundTreasures.Add(item);
                }
            }

            return foundTreasures[Random.Range(0, foundTreasures.Count)];
        }
    }
}
