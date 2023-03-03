using General;
using SaveSystem;
using UnityEngine;

namespace Needs
{
    public class NeedsManager : MonoBehaviour
    {
        [SerializeField] float _needUseAmount = 50f;

        NeedsSystem _needsSystem;
        bool _isHungry = true;
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
            if (_isHungry)
            {
                _needsSystem.GetNeed(NeedType.Hunger).OnCoreUse += Hunger_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.Hunger).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isHungry = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.Hunger).GetTotalRingNormalizedValue() == 1)
                {
                    _isHungry = true;
                }
            }

            if (_wantsFriend)
            {
                _needsSystem.GetNeed(NeedType.WantsNewFriend).OnCoreUse += WantsFriend_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.WantsNewFriend).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFriend = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.WantsNewFriend).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsFriend = true;
                }
            }

            if (_wantsInterior)
            {
                _needsSystem.GetNeed(NeedType.WantsNewInterior).OnCoreUse += WantsInterior_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.WantsNewInterior).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsInterior = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.WantsNewInterior).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsInterior = true;
                }
            }

            if (_isSick)
            {
                _needsSystem.GetNeed(NeedType.Sickness).OnCoreUse += Sick_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.Sickness).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isSick = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.Sickness).GetTotalRingNormalizedValue() == 1)
                {
                    _isSick = true;
                }
            }

            if (_isDivorcing)
            {
                _needsSystem.GetNeed(NeedType.FailedRelationship).OnCoreUse += Divorce_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.FailedRelationship).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isDivorcing = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.FailedRelationship).GetTotalRingNormalizedValue() == 1)
                {
                    _isDivorcing = true;
                }
            }

            if (_wantsFight)
            {
                _needsSystem.GetNeed(NeedType.Fight).OnCoreUse += Fight_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.Fight).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFight = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.Fight).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsFight = true;
                }
            }

            if (_wantsDate)
            {
                _needsSystem.GetNeed(NeedType.Date).OnCoreUse += Date_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.Date).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsDate = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.Date).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsDate = true;
                }
            }
        }

        private void Date_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Date;
        }

        private void Fight_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Angry;
        }

        private void Divorce_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Sad;
        }

        private void Sick_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Sick;
        }

        private void WantsInterior_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Buy;
        }

        private void WantsFriend_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.MakeFriend;
        }

        private void Hunger_OnCoreUse(object sender, System.EventArgs e)
        {
            CharacterData character = PopulationManager.Instance.ReturnDouble(gameObject.name.Substring(0, gameObject.name.Length - 7));
            character.CurrentState = DoubleState.Hungry;
        }
    }
}
