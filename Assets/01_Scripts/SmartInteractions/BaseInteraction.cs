using General;
using TraitSystem;
using UnityEngine;
using UnityEngine.Events;

namespace SmartInteractions
{
    public abstract class BaseInteraction : MonoBehaviour
    {
        [SerializeField] protected string _displayName;
        [SerializeField] protected InteractionType _interactionType = InteractionType.Instantaneous;
        [SerializeField] protected float _duration = 0f;
        [SerializeField] protected InteractionStatChange[] _statChanges;
        [SerializeField]
        InteractionOutcome[] _outcomes = new InteractionOutcome[] { new InteractionOutcome()
            {
                weighting = 1f,
                description = "",

            }
        };
        bool _outcomeWeightingsNormalised = false;


        public string DisplayName => _displayName;
        public InteractionType InteractionType => _interactionType;
        public float Duration => _duration;
        public InteractionStatChange[] StatChanges => _statChanges;
        public abstract bool CanPerform();
        public abstract bool LockInteraction(HumanAIBase performer);
        public abstract bool UnlockInteraction(HumanAIBase performer);
        public abstract bool Perform(HumanAIBase performer, UnityAction<BaseInteraction> onCompleted);

        public bool ApplyInteractionEffects(HumanAIBase performer, float proportion, bool rollForOutcomes)
        {
            InteractionOutcome selectedOutcome = null;
            bool abandonInteraction = false;

            if (rollForOutcomes && _outcomes.Length > 0)
            {
                if (!_outcomeWeightingsNormalised)
                {
                    _outcomeWeightingsNormalised = true;
                    float weightingSum = 0f;

                    foreach (var outcome in _outcomes)
                    {
                        weightingSum += outcome.weighting;
                    }

                    foreach (var outcome in _outcomes)
                    {
                        outcome.NormalisedWeighting = outcome.weighting / weightingSum;
                    }
                }

                float ramdomRoll = Random.value;

                foreach (var outcome in _outcomes)
                {
                    if (ramdomRoll <= outcome.NormalisedWeighting)
                    {
                        selectedOutcome = outcome;

                        if (selectedOutcome.abandonInteraction)
                        {
                            abandonInteraction = true;
                        }

                        break;
                    }
                    ramdomRoll -= outcome.NormalisedWeighting;
                }
            }

            float statMultiplier = selectedOutcome != null ? selectedOutcome.statMultiplier : 1f;

            foreach (var statChange in _statChanges)
            {
                performer.UpdateIndividualStat(statChange._linkedStat, statMultiplier * statChange._value * proportion, TargetType.Impact);
            }

            if (selectedOutcome != null)
            {
                if (!string.IsNullOrEmpty(selectedOutcome.description.Value))
                {
                    Debug.Log($"Outcome was {selectedOutcome.description.Value}");
                }

                foreach (var statChange in selectedOutcome.statChanges)
                {
                    performer.UpdateIndividualStat(statChange._linkedStat, statChange._value * proportion, TargetType.Impact);
                }

                if (selectedOutcome.memoriesCaused.Length > 0)
                {
                    performer.AddMemories(selectedOutcome.memoriesCaused);
                }
            }

            return !abandonInteraction;
        }
    }
}