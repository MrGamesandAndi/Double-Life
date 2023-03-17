using General;
using Relationships;
using System;
using System.Collections.Generic;
using TraitSystem;
using UnityEngine;

namespace Needs
{
    public class NeedsManager : MonoBehaviour
    {
        [SerializeField] float _needUseAmount = 50f;

        public int characterId;
        NeedsSystem _needsSystem;

        float _furnitureMultiplier = 1f;
        float _loveMultiplier = 1f;
        float _dateMultiplier = 1f;
        float _fightMultiplier = 1f;
        float _hungerMultiplier = 1f;
        float _friendMultiplier = 1f;
        float _depressionMultiplier = 1f;
        float _sicknessMultiplier = 1f;
        float _breakUpMultiplier = 1f;
        List<Trait> traits;

        private void Awake()
        {
            _needsSystem = new NeedsSystem();
        }

        public void LinkCharacterData(int id)
        {
            characterId = id;
        }

        public void SetMultipliers()
        {
            traits = new List<Trait>();
            traits = BodyPartsCollection.Instance.ReturnTraitsFromCharacterData(PopulationManager.Instance.ReturnDouble(characterId).Traits);

            _hungerMultiplier = ReturnTypeMultiplier(NeedType.Hunger);
            _loveMultiplier = ReturnTypeMultiplier(NeedType.ConfessLove);
            _dateMultiplier = ReturnTypeMultiplier(NeedType.HaveDate);
            _fightMultiplier = ReturnTypeMultiplier(NeedType.HaveFight);
            _hungerMultiplier = ReturnTypeMultiplier(NeedType.Hunger);
            _friendMultiplier = ReturnTypeMultiplier(NeedType.MakeFriend);
            _depressionMultiplier = ReturnTypeMultiplier(NeedType.HaveDepression);
            _sicknessMultiplier = ReturnTypeMultiplier(NeedType.Sickness);
            _furnitureMultiplier = ReturnTypeMultiplier(NeedType.BuyFurniture);
        }

        private float ReturnTypeMultiplier(NeedType type)
        {
            foreach (var trait in traits)
            {
                if(trait.type == type)
                {
                    return trait._decayRateMultiplier;
                }
            }

            return 1;
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
            if (RelationshipSystem.Instance.CheckIfLoveInterestExists(PopulationManager.Instance.ReturnDouble(characterId)))
            {
                PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.BreakUp;
            }
            else
            {
                NeedCompleted(NeedType.BreakUp);
            }
        }

        private void OnNeedReset(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Happy;
        }

        public void NeedCompleted(NeedType need)
        {
            OnNeedReset(this, EventArgs.Empty);
            _needsSystem.GetNeed(need).ResetNeed();
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
            if (RelationshipSystem.Instance.CheckIfDoubleHasRelationships(PopulationManager.Instance.ReturnDouble(characterId)))
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
            if (RelationshipSystem.Instance.CheckIfDoubleHasRelationships(PopulationManager.Instance.ReturnDouble(characterId)))
            {
                PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Confession;
            }
            else
            {
                NeedCompleted(NeedType.ConfessLove);
            }
        }

        private void Date_OnCoreUse(object sender, EventArgs e)
        {
            if (RelationshipSystem.Instance.CheckIfLoveInterestExists(PopulationManager.Instance.ReturnDouble(characterId)))
            {
                PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Date;
            }
            else
            {
                NeedCompleted(NeedType.HaveDate);
            }
        }

        private void Fight_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Angry;
        }

        private void Sadness_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Sad;
        }

        private void Sick_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Sick;
        }

        private void WantsInterior_OnCoreUse(object sender, EventArgs e)
        {
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Buy;
        }

        private void WantsFriend_OnCoreUse(object sender, EventArgs e)
        {
            
                if (PopulationManager.Instance.DoublesList.Count > 1)
                {
                    if (GameManager.Instance.currentLoadedDouble.Relationships.Count <= PopulationManager.Instance.DoublesList.Count - 1)
                    {
                        PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.MakeFriend;
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
            PopulationManager.Instance.ReturnDouble(characterId).CurrentState = DoubleState.Hungry;
        }
    }
}
