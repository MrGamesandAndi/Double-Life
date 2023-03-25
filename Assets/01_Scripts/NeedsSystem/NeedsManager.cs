using General;
using Relationships;
using System;
using UnityEngine;

namespace Needs
{
    public class NeedsManager : MonoBehaviour
    {
        [SerializeField] float _needUseAmount = 50f;

        public int _characterId;
        NeedsSystem _needsSystem;

        public float _furnitureMultiplier;
        public float _loveMultiplier;
        public float _dateMultiplier;
        public float _fightMultiplier;
        public float _hungerMultiplier;
        public float _friendMultiplier;
        public float _depressionMultiplier;
        public float _sicknessMultiplier;
        public float _breakUpMultiplier;

        private void Awake()
        {
            _needsSystem = new NeedsSystem();
        }

        public void LinkCharacterData(int id)
        {
            _characterId = id;
        }

        public void SetMultipliers()
        {
            _hungerMultiplier = ReturnTypeMultiplier(NeedType.Hunger);
            _loveMultiplier = ReturnTypeMultiplier(NeedType.ConfessLove);
            _dateMultiplier = ReturnTypeMultiplier(NeedType.HaveDate);
            _fightMultiplier = ReturnTypeMultiplier(NeedType.HaveFight);
            _friendMultiplier = ReturnTypeMultiplier(NeedType.MakeFriend);
            _depressionMultiplier = ReturnTypeMultiplier(NeedType.HaveDepression);
            _sicknessMultiplier = ReturnTypeMultiplier(NeedType.Sickness);
            _furnitureMultiplier = ReturnTypeMultiplier(NeedType.BuyFurniture);
        }

        private float ReturnTypeMultiplier(NeedType type)
        {
            foreach (var trait in BodyPartsCollection.Instance.ReturnTraitsFromCharacterData(PopulationManager.Instance.ReturnDouble(_characterId).Traits))
            {
                if (trait.type == type)
                {
                    return trait._decayRateMultiplier;
                }
            }

            return 1f;
        }

        private void Start()
        {
            _needsSystem.GetNeed(NeedType.Hunger).OnCoreUse += Hunger_OnCoreUse;
            _needsSystem.GetNeed(NeedType.Hunger).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.TalkToFriend).OnCoreUse += Talk_OnCoreUse;
            _needsSystem.GetNeed(NeedType.TalkToFriend).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.MakeFriend).OnCoreUse += WantsFriend_OnCoreUse;
            _needsSystem.GetNeed(NeedType.MakeFriend).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.BuyFurniture).OnCoreUse += WantsInterior_OnCoreUse;
            _needsSystem.GetNeed(NeedType.BuyFurniture).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.Sickness).OnCoreUse += Sick_OnCoreUse;
            _needsSystem.GetNeed(NeedType.Sickness).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.HaveDepression).OnCoreUse += Sadness_OnCoreUse;
            _needsSystem.GetNeed(NeedType.HaveDepression).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.HaveFight).OnCoreUse += Fight_OnCoreUse;
            _needsSystem.GetNeed(NeedType.HaveFight).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.HaveDate).OnCoreUse += Date_OnCoreUse;
            _needsSystem.GetNeed(NeedType.HaveDate).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.ConfessLove).OnCoreUse += Confess_OnCoreUse;
            _needsSystem.GetNeed(NeedType.ConfessLove).OnNeedReset += OnNeedReset;

            _needsSystem.GetNeed(NeedType.BreakUp).OnCoreUse += BreakUp_OnCoreUse;
            _needsSystem.GetNeed(NeedType.BreakUp).OnNeedReset += OnNeedReset;
        }

        private void BreakUp_OnCoreUse(object sender, EventArgs e)
        {
            if (RelationshipSystem.Instance.CheckIfLoveInterestExists(_characterId))
            {
                PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.BreakUp;
            }
            else
            {
                NeedCompleted(NeedType.BreakUp);
            }
        }

        private void OnNeedReset(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Happy;
        }

        public void NeedCompleted(NeedType need)
        {
            OnNeedReset(this, EventArgs.Empty);
            _needsSystem.GetNeed(need).ResetNeed();
        }

        public void ResetAllNeeds()
        {
            NeedCompleted(NeedType.Hunger);
            NeedCompleted(NeedType.MakeFriend);
            NeedCompleted(NeedType.BuyFurniture);
            NeedCompleted(NeedType.Sickness);
            NeedCompleted(NeedType.HaveDepression);
            NeedCompleted(NeedType.HaveFight);
            NeedCompleted(NeedType.HaveDate);
            NeedCompleted(NeedType.ConfessLove);
            NeedCompleted(NeedType.TalkToFriend);
        }

        private void Update()
        {
            _needsSystem.GetNeed(NeedType.Hunger).UseNeed(_needUseAmount, _hungerMultiplier);
            _needsSystem.GetNeed(NeedType.MakeFriend).UseNeed(_needUseAmount, _friendMultiplier);
            _needsSystem.GetNeed(NeedType.BuyFurniture).UseNeed(_needUseAmount, _furnitureMultiplier);
            _needsSystem.GetNeed(NeedType.Sickness).UseNeed(_needUseAmount, _sicknessMultiplier);
            _needsSystem.GetNeed(NeedType.HaveDepression).UseNeed(_needUseAmount, _depressionMultiplier);
            _needsSystem.GetNeed(NeedType.HaveFight).UseNeed(_needUseAmount, _fightMultiplier);
            _needsSystem.GetNeed(NeedType.HaveDate).UseNeed(_needUseAmount, _dateMultiplier);
            _needsSystem.GetNeed(NeedType.ConfessLove).UseNeed(_needUseAmount, _loveMultiplier);
            _needsSystem.GetNeed(NeedType.TalkToFriend).UseNeed(_needUseAmount, _friendMultiplier);
        }

        private void Talk_OnCoreUse(object sender, EventArgs e)
        {
            if (RelationshipSystem.Instance.CheckIfDoubleHasRelationships(PopulationManager.Instance.ReturnDouble(_characterId)))
            {
                RelationshipSystem.Instance.TalkToRandomExistingFriend();
            }

            NeedCompleted(NeedType.TalkToFriend);
        }

        public Need GetNeed(NeedType type)
        {
            return _needsSystem.GetNeed(type);
        }

        private void Confess_OnCoreUse(object sender, EventArgs e)
        {
            if (!RelationshipSystem.Instance.CheckIfLoveInterestExists(_characterId))
            {
                if (RelationshipSystem.Instance.CheckIfDoubleHasRelationships(PopulationManager.Instance.ReturnDouble(_characterId)))
                {
                    PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Confession;
                }
                else
                {
                    NeedCompleted(NeedType.ConfessLove);
                }
            }
            else
            {
                NeedCompleted(NeedType.ConfessLove);
            }
        }

        private void Date_OnCoreUse(object sender, EventArgs e)
        {
            if (RelationshipSystem.Instance.CheckIfLoveInterestExists(_characterId))
            {
                PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Date;
            }
            else
            {
                NeedCompleted(NeedType.HaveDate);
            }
        }

        private void Fight_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Angry;
        }

        private void Sadness_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Sad;
        }

        private void Sick_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Sick;
        }

        private void WantsInterior_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Buy;
        }

        private void WantsFriend_OnCoreUse(object sender, EventArgs e)
        {
            if (PopulationManager.Instance.DoublesList.Count > 1)
            {
                if (GameManager.Instance.currentLoadedDouble.Relationships.Count <= PopulationManager.Instance.DoublesList.Count - 1)
                {
                    PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.MakeFriend;
                }
                else
                {
                    NeedCompleted(NeedType.MakeFriend);
                }
            }
            else
            {
                NeedCompleted(NeedType.MakeFriend);
            }
                
        }

        private void Hunger_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(_characterId).CurrentState = DoubleState.Hungry;
        }
    }
}
