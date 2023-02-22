using System.Collections.Generic;

namespace Needs
{
    public class NeedsSystem
    {
       Dictionary<NeedType, Need> _needDictionary;

        public NeedsSystem()
        {
            _needDictionary = new Dictionary<NeedType, Need>();
            _needDictionary[NeedType.Hunger] = new Need();
            _needDictionary[NeedType.WantsNewFriend] = new Need();
            _needDictionary[NeedType.WantsNewInterior] = new Need();
            _needDictionary[NeedType.Sickness] = new Need();
            _needDictionary[NeedType.FailedRelationship] = new Need();
            _needDictionary[NeedType.Fight] = new Need();
            _needDictionary[NeedType.Date] = new Need();
        }

        public Need GetNeed(NeedType needType)
        {
            return _needDictionary[needType];
        }

        public void Update()
        {
            foreach (NeedType needType in _needDictionary.Keys)
            {
                GetNeed(needType).Update();
            }
        }
    }
}