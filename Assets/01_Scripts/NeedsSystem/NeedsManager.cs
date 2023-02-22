using UnityEngine;

namespace Needs
{
    public class NeedsManager : MonoBehaviour
    {
        [SerializeField] float _needUseAmount = 50f;
        NeedsSystem _needsSystem;
        bool _isHungry;
        bool _wantsFriend;
        bool _wantsInterior;
        bool _isSick;
        bool _isDivorcing;
        bool _wantsFight;
        bool _wantsDate;

        private void Awake()
        {
            _needsSystem = new NeedsSystem();
        }

        private void Update()
        {
            if (!_isHungry)
            {
                if (!_needsSystem.GetNeed(NeedType.Hunger).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isHungry = true;
                }
            }

            if (!_wantsFriend)
            {
                if (!_needsSystem.GetNeed(NeedType.WantsNewFriend).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFriend = true;
                }
            }

            if (!_wantsInterior)
            {
                if (!_needsSystem.GetNeed(NeedType.WantsNewInterior).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsInterior = true;
                }
            }

            if (!_isSick)
            {
                if (!_needsSystem.GetNeed(NeedType.Sickness).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isSick = true;
                }
            }

            if (!_isDivorcing)
            {
                if (!_needsSystem.GetNeed(NeedType.FailedRelationship).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isDivorcing = true;
                }
            }

            if (!_wantsFight)
            {
                if (!_needsSystem.GetNeed(NeedType.Fight).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFight = true;
                }
            }

            if (!_wantsDate)
            {
                if (!_needsSystem.GetNeed(NeedType.Date).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsDate = true;
                }
            }

            _needsSystem.Update();
        }
    }
}
