using Blackboards;
using DialogueSystem;
using MemorySystem;
using Stats;
using System.Collections.Generic;
using System.Linq;
using TraitSystem;
using UnityEngine;

namespace SmartInteractions
{
    public class SimpleAI : HumanAIBase
    {
        [SerializeField] protected float _pickInteractionInterval = 2f;
        [SerializeField] protected float _defaultInteractionScore = 0f;
        [SerializeField] protected int _interactionPickSize = 5;
        [SerializeField] protected bool _avoidInUseObjects = true;

        protected float _timeUntilNextInteractionPicked = -1f;

        protected override void Update()
        {
            base.Update();

            if (CurrentInteraction == null)
            {
                _timeUntilNextInteractionPicked -= Time.deltaTime;

                if (_timeUntilNextInteractionPicked <= 0)
                {
                    _timeUntilNextInteractionPicked = _pickInteractionInterval;
                    PickBestInteraction();
                }
            }
        }

        public void PickBestInteraction()
        {
            HouseholdBlackboard.TryGetGeneric(BlackboardKey.Household_Objects_In_Use, out List<GameObject> objectsInUse, null);
            List<ScoredInteractions> unsortedInteractions = new List<ScoredInteractions>();

            foreach (var smartInteraction in SmartInteractionManager.Instance.RegisteredObjects)
            {
                foreach (var interaction in smartInteraction.Interactions)
                {
                    if (!interaction.CanPerform())
                    {
                        continue;
                    }

                    if (_avoidInUseObjects && objectsInUse != null && objectsInUse.Contains(interaction.gameObject))
                    {
                        continue;
                    }

                    float score = ScoreInteraction(interaction);
                    unsortedInteractions.Add(new ScoredInteractions()
                    {
                        targetInteraction = smartInteraction,
                        interaction = interaction,
                        score = score
                    });
                }
            }

            if (unsortedInteractions.Count == 0)
            {
                return;
            }

            var sortedInteractions = unsortedInteractions.OrderByDescending(scoredInteractions => scoredInteractions.score).ToList();
            int maxIndex = Mathf.Min(_interactionPickSize, sortedInteractions.Count);
            var selectedIndex = Random.Range(0, maxIndex);
            var selectedObject = sortedInteractions[selectedIndex].targetInteraction;
            var selectedInteraction = sortedInteractions[selectedIndex].interaction;
            CurrentInteraction = selectedInteraction;
            CurrentInteraction.LockInteraction(this);
            _startedPerforming = false;

            Debug.Log($"Going to {CurrentInteraction.DisplayName} at {selectedObject.DisplayName}"); 
        }

        private float ScoreInteraction(BaseInteraction interaction)
        {
            if (interaction.StatChanges.Length == 0)
            {
                return _defaultInteractionScore;
            }

            List<MemoryFragment> recentMemories = IndividualBlackboard.GetGeneric<List<MemoryFragment>>(BlackboardKey.Memories_Short_Term);
            List<MemoryFragment> permanentMemories = IndividualBlackboard.GetGeneric<List<MemoryFragment>>(BlackboardKey.Memories_Long_Term);
            float score = 0f;

            foreach (var change in interaction.StatChanges)
            {
                score += ScoreChange(change._linkedStat, change._value, recentMemories, permanentMemories);
            }

            return score;
        }

        private float ScoreChange(AIStat linkedStat, float amount, List<MemoryFragment> recentMemories, List<MemoryFragment> permanentMemories)
        {
            float currentValue = GetStatValue(linkedStat);
            ModifyValueBasedOnMemories(currentValue, linkedStat, recentMemories);
            ModifyValueBasedOnMemories(currentValue, linkedStat, permanentMemories);
            return (1f - currentValue) * ApplyTraitsTo(linkedStat, TargetType.Score, amount);
        }

        private float ModifyValueBasedOnMemories(float currentValue, AIStat linkedStat, List<MemoryFragment> memories)
        {
            foreach (var memory in memories)
            {
                foreach (var change in memory.StatChanges)
                {
                    if (change._linkedStat == linkedStat)
                    {
                        currentValue *= change._value;
                    }
                }
            }

            return currentValue;
        }
    }
}
