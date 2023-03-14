using General;
using SaveSystem;
using System;
using UnityEngine;

namespace Needs
{
    public class NeedsManager : MonoBehaviour
    {
        [SerializeField] float _needUseAmount = 50f;

        CharacterData _characterData;
        NeedsSystem _needsSystem;
        bool _isHungry = true;
        bool _wantsFriend = true;
        bool _wantsInterior = true;
        bool _isSick = true;
        bool _isDivorcing = true;
        bool _wantsFight = true;
        bool _wantsDate = true;
        bool _wantsLove = true;

        private void Awake()
        {
            _needsSystem = new NeedsSystem();
        }

        public void LinkCharacterData(CharacterData characterData)
        {
            _characterData = characterData;
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
                _needsSystem.GetNeed(NeedType.MakeFriend).OnCoreUse += WantsFriend_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.MakeFriend).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFriend = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.MakeFriend).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsFriend = true;
                }
            }

            if (_wantsInterior)
            {
                _needsSystem.GetNeed(NeedType.BuyFurniture).OnCoreUse += WantsInterior_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.BuyFurniture).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsInterior = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.BuyFurniture).GetTotalRingNormalizedValue() == 1)
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
                _needsSystem.GetNeed(NeedType.HaveDepression).OnCoreUse += Divorce_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.HaveDepression).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _isDivorcing = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.HaveDepression).GetTotalRingNormalizedValue() == 1)
                {
                    _isDivorcing = true;
                }
            }

            if (_wantsFight)
            {
                _needsSystem.GetNeed(NeedType.HaveFight).OnCoreUse += Fight_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.HaveFight).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsFight = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.HaveFight).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsFight = true;
                }
            }

            if (_wantsDate)
            {
                _needsSystem.GetNeed(NeedType.HaveDate).OnCoreUse += Date_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.HaveDate).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsDate = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.HaveDate).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsDate = true;
                }
            }

            if (_wantsLove)
            {
                _needsSystem.GetNeed(NeedType.ConfessLove).OnCoreUse += Confess_OnCoreUse;

                if (!_needsSystem.GetNeed(NeedType.ConfessLove).TryUseNeed(_needUseAmount * Time.deltaTime))
                {
                    _wantsLove = false;
                }
            }
            else
            {
                if (_needsSystem.GetNeed(NeedType.ConfessLove).GetTotalRingNormalizedValue() == 1)
                {
                    _wantsLove = true;
                }
            }
        }

        private void Confess_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Confession;
        }

        private void Date_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Date;
        }

        private void Fight_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Angry;
        }

        private void Divorce_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Sad;
        }

        private void Sick_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Sick;
        }

        private void WantsInterior_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Buy;
        }

        private void WantsFriend_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.MakeFriend;
        }

        private void Hunger_OnCoreUse(object sender, EventArgs e)
        {
            _characterData.CurrentState = DoubleState.Hungry;
        }
    }
}
