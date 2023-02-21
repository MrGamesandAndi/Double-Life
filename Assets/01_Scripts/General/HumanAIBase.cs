using Blackboards;
using MemorySystem;
using Stats;
using System.Collections.Generic;
using TraitSystem;
using UnityEngine;

namespace SmartInteractions
{
    public class HumanAIBase : MonoBehaviour
    {
        [SerializeField] int _householdID = 1;
        [SerializeField] AIStatConfiguration[] _stats;
        [SerializeField] FeedbackUIPanel _linkedUI;
        [SerializeField] List<Trait> _traits;
        [SerializeField] int _longTermMemoryThreshold = 3;

        protected Dictionary<AIStat, float> _decayRates = new Dictionary<AIStat, float>();
        protected Dictionary<AIStat, AIStatPanel> _statUIPanels = new Dictionary<AIStat, AIStatPanel>();
        protected bool _startedPerforming = false;

        public Blackboard IndividualBlackboard { get; protected set; }
        public Blackboard HouseholdBlackboard { get; protected set; }

        protected BaseInteraction CurrentInteraction
        {
            get
            {
                BaseInteraction interaction = null;
                IndividualBlackboard.TryGetGeneric(BlackboardKey.Character_Focus_Object, out interaction, null);
                return interaction;
            }

            set
            {
                BaseInteraction previousInteraction = null;
                IndividualBlackboard.TryGetGeneric(BlackboardKey.Character_Focus_Object, out previousInteraction, null);
                IndividualBlackboard.SetGeneric(BlackboardKey.Character_Focus_Object, value);
                List<GameObject> objectsInUse = null;
                HouseholdBlackboard.TryGetGeneric(BlackboardKey.Household_Objects_In_Use, out objectsInUse, null);

                if (value != null)
                {
                    if (objectsInUse == null)
                    {
                        objectsInUse = new List<GameObject>();
                    }

                    if (!objectsInUse.Contains(value.gameObject))
                    {
                        objectsInUse.Add(value.gameObject);
                        HouseholdBlackboard.SetGeneric(BlackboardKey.Household_Objects_In_Use, objectsInUse);
                    }
                }
                else if (objectsInUse != null)
                {
                    if (objectsInUse.Remove(previousInteraction.gameObject))
                    {
                        HouseholdBlackboard.SetGeneric(BlackboardKey.Household_Objects_In_Use, objectsInUse);
                    }
                }
            }
        }

        public List<Trait> Traits { get => _traits; set => _traits = value; }

        protected virtual void Start()
        {
            HouseholdBlackboard = BlackboardManager.Instance.GetSharedBlackboard(_householdID);
            IndividualBlackboard = BlackboardManager.Instance.GetIndividualBlackboard(this);
            IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Long_Term, new List<MemoryFragment>());
            IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Short_Term, new List<MemoryFragment>());

            foreach (var statConfig in _stats)
            {
                var linkedStat = statConfig.LinkedStat;
                float initialValue = statConfig.OverrideDefaults ? statConfig.OverrideInitialValue : linkedStat.InitialValue;
                float decayRate = statConfig.OverrideDefaults ? statConfig.OverrideDecayRate : linkedStat.DecayRate;
                _decayRates[linkedStat] = decayRate;
                IndividualBlackboard.SetStat(linkedStat, initialValue);

                if (linkedStat.IsVisible && _statUIPanels.Count > 0)
                {
                    _statUIPanels[linkedStat] = _linkedUI.AddStat(linkedStat, initialValue);
                }
            }
        }

        protected virtual void Update()
        {
            if (CurrentInteraction != null)
            {
                if (!_startedPerforming)
                {
                    _startedPerforming = true;
                    //CurrentInteraction.Perform(this, OnInteractionFinished);
                }
            }

            foreach (var statConfig in _stats)
            {
                UpdateIndividualStat(statConfig.LinkedStat, -_decayRates[statConfig.LinkedStat] * Time.deltaTime, TargetType.Decay_Rate);
            }

            List<MemoryFragment> recentMemories = IndividualBlackboard.GetGeneric<List<MemoryFragment>>(BlackboardKey.Memories_Short_Term);
            bool memoriesChanged = false;

            for (int i = recentMemories.Count - 1; i >= 0; i--)
            {
                if (!recentMemories[i].Tick(Time.deltaTime))
                {
                    recentMemories.RemoveAt(i);
                    memoriesChanged = true;
                }
            }

            if (memoriesChanged)
            {
                IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Short_Term, recentMemories);
            }
        }

        protected float ApplyTraitsTo(AIStat targetStat, TargetType targetType, float currentValue)
        {
            foreach (var trait in _traits)
            {
                currentValue = trait.Apply(targetStat, targetType, currentValue);
            }

            return currentValue;
        }

        protected virtual void OnInteractionFinished(BaseInteraction interaction)
        {
            interaction.UnlockInteraction(this);
            CurrentInteraction = null;

            Debug.Log($"Finished {interaction.DisplayName}");
        }

        public void UpdateIndividualStat(AIStat linkedStat, float amount, TargetType targetType)
        {
            float adjustedAmount = ApplyTraitsTo(linkedStat, targetType, amount);
            float newValue = Mathf.Clamp01(GetStatValue(linkedStat) + adjustedAmount);
            IndividualBlackboard.SetStat(linkedStat, newValue);

            if (linkedStat.IsVisible && _statUIPanels.Count > 0)
            {
                _statUIPanels[linkedStat].OnStatChanged(newValue);
            }
        }

        public float GetStatValue(AIStat linkedStat)
        {
            return IndividualBlackboard.GetStat(linkedStat);
        }

        public void AddMemories(MemoryFragment[] memoriesToAdd)
        {
            foreach (var memory in memoriesToAdd)
            {
                AddMemory(memory);
            }
        }

        protected void AddMemory(MemoryFragment memoryToAdd)
        {
            List<MemoryFragment> permanentMemories = IndividualBlackboard.GetGeneric<List<MemoryFragment>>(BlackboardKey.Memories_Long_Term);
            MemoryFragment memoryToCancel = null;

            foreach (var memory in permanentMemories)
            {
                if (memoryToAdd.IsSimilarTo(memory))
                {
                    return;
                }

                if (memory.IsCancelledBy(memoryToAdd))
                {
                    memoryToCancel = memory;
                }
            }

            if (memoryToCancel != null)
            {
                permanentMemories.Remove(memoryToCancel);
                IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Long_Term, permanentMemories);
            }

            List<MemoryFragment> recentMemories = IndividualBlackboard.GetGeneric<List<MemoryFragment>>(BlackboardKey.Memories_Short_Term);
            MemoryFragment existingRecentMemory = null;

            foreach (var memory in recentMemories)
            {
                if (memoryToAdd.IsSimilarTo(memory))
                {
                    existingRecentMemory = memory;
                }

                if (memory.IsCancelledBy(memoryToAdd))
                {
                    memoryToCancel = memory;
                }
            }

            if (memoryToCancel != null)
            {
                recentMemories.Remove(memoryToCancel);
                IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Short_Term, recentMemories);
            }

            if (existingRecentMemory == null)
            {
                Debug.Log($"Added memory {memoryToAdd.Name.Value}");
                recentMemories.Add(memoryToAdd.Duplicate());
                IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Short_Term, recentMemories);
            }
            else
            {
                Debug.Log($"Reinforced memory {memoryToAdd.Name.Value}");
                existingRecentMemory.Reinforce(memoryToAdd);

                if (existingRecentMemory.Occurrences >= _longTermMemoryThreshold)
                {
                    permanentMemories.Add(existingRecentMemory);
                    recentMemories.Remove(existingRecentMemory);
                    IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Short_Term, recentMemories);
                    IndividualBlackboard.SetGeneric(BlackboardKey.Memories_Long_Term, permanentMemories);
                    Debug.Log($"Memory {existingRecentMemory.Name.Value} become permanent.");
                }
            }
        }
    }
}