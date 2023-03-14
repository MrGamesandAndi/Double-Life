using CharacterCreator;
using Needs;
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

        public List<Trait> ReturnTraitFromCharacterData(List<int> characterTraits)
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

        public List<Trait> ReturnTraitsByType(NeedType type)
        {
            List<Trait> returnedTraits = new List<Trait>();

            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].type == type)
                {
                    returnedTraits.Add(traits[i]);
                }
            }

            return returnedTraits;
        }
    }
}
