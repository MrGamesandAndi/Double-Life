using Localisation;
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
        public TraitElement[] impacts;
    }
}