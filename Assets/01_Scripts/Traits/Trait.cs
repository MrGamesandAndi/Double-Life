using Localisation;
using Needs;
using UnityEngine;

namespace TraitSystem
{
    [CreateAssetMenu(menuName = "AI/Trait", fileName = "Trait")]
    public class Trait : ScriptableObject
    {
        public int id;
        public LocalisedString displayName;
        public LocalisedString description;
        public Trait opossiteTrait;
        [Range(0.5f, 1.5f)] public float _decayRateMultiplier = 1f;
        public NeedType type;
    }
}