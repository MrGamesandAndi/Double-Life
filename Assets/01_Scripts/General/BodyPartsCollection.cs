using CharacterCreator;
using Stats;
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
        public List<AIStat> stats;

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
    }
}
