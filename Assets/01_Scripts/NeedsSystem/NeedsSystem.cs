using System.Collections.Generic;

namespace Needs
{
    public class NeedsSystem
    {
       Dictionary<NeedType, Need> _needDictionary;

        public NeedsSystem()
        {
            _needDictionary = new Dictionary<NeedType, Need>();
            _needDictionary[NeedType.BuyFurniture] = new Need();
            _needDictionary[NeedType.ConfessLove] = new Need();
            _needDictionary[NeedType.HaveDate] = new Need();
            _needDictionary[NeedType.HaveFight] = new Need();
            _needDictionary[NeedType.Hunger] = new Need();
            _needDictionary[NeedType.MakeFriend] = new Need();
            _needDictionary[NeedType.HaveDepression] = new Need();
            _needDictionary[NeedType.Sickness] = new Need();
            _needDictionary[NeedType.TalkToFriend] = new Need();
        }

        public Need GetNeed(NeedType needType)
        {
            return _needDictionary[needType];
        }
    }
}