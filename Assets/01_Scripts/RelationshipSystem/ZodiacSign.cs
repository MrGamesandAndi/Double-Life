using Localisation;
using System.Collections.Generic;
using UnityEngine;

namespace Relationships
{
    [CreateAssetMenu(menuName = "ZodiacSign")]
    public class ZodiacSign : ScriptableObject
    {
        public int id;
        public LocalisedString zodiacName;
        public List<ZodiacSign> compatibleSigns;
    }
}
