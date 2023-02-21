using Stats;
using System;

namespace SmartInteractions
{
    [Serializable]
    public class InteractionStatChange
    {
        public AIStat _linkedStat;
        public float _value;
    }
}