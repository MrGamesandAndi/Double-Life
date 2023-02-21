using Localisation;
using Stats;
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

        public float Apply(AIStat targetStat, TargetType targetType, float currentValue)
        {
            foreach (var impact in impacts)
            {
                currentValue = impact.Apply(targetStat, targetType, currentValue);
            }

            return currentValue;
        }
    }
}