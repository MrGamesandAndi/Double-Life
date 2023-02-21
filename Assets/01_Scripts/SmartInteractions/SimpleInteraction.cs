using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SmartInteractions
{
    public class SimpleInteraction : BaseInteraction
    {
        [SerializeField] protected int _maxSimultaneousUsers = 1;
        protected Dictionary<HumanAIBase,PerformerInfo> currentPerformers = new Dictionary<HumanAIBase, PerformerInfo>();
        protected List<HumanAIBase> performersToCleanup = new List<HumanAIBase>();

        public int _numCurrentUsers => currentPerformers.Count;

        public override bool CanPerform()
        {
            return _numCurrentUsers < _maxSimultaneousUsers;
        }

        public override bool LockInteraction(HumanAIBase performer)
        {
            if (_numCurrentUsers >= _maxSimultaneousUsers)
            {
                Debug.LogError($"{performer.name} tried to lock {DisplayName} which is already at max users.");
                return false;
            }

            if (currentPerformers.ContainsKey(performer))
            {
                Debug.LogError($"{performer.name} tried to lock {DisplayName} multiple times.");
                return false;
            }

            currentPerformers[performer] = null;
            return true;
        }

        public override bool Perform(HumanAIBase performer, UnityAction<BaseInteraction> onCompleted)
        {
            if (!currentPerformers.ContainsKey(performer))
            {
                Debug.LogError($"{performer.name} is trying to perform an interaction {DisplayName} that they have not locked.");
                return false;
            }

            if (InteractionType == InteractionType.Instantaneous)
            {
                if(_statChanges.Length > 0)
                {
                    ApplyInteractionEffects(performer, 1f, true);
                }

                OnInteractionCompleted(performer, onCompleted);
            }
            else if (InteractionType == InteractionType.OverTime || InteractionType == InteractionType.AfterTime)
            {
                currentPerformers[performer] = new PerformerInfo()
                {
                    ElapseTime = 0,
                    onCompleted = onCompleted
                };
            }

            return true;
        }

        protected void OnInteractionCompleted(HumanAIBase performer, UnityAction<BaseInteraction> onCompleted)
        {
            onCompleted.Invoke(this);

            if (!performersToCleanup.Contains(performer))
            {
                performersToCleanup.Add(performer);
                Debug.LogWarning($"{performer.name} did not unlock the interaction in their OnCompleted handler for {DisplayName}");
            }
        }


        public override bool UnlockInteraction(HumanAIBase performer)
        {
            if (currentPerformers.ContainsKey(performer))
            {
                performersToCleanup.Add(performer);
                return true;
            }

            Debug.LogError($"{performer.name} is trying to unlock an interaction {DisplayName} they have not locked.");
            return false;
        }

        protected virtual void Update()
        {
            foreach (var keyValuePair in currentPerformers)
            {
                HumanAIBase performer = keyValuePair.Key;
                PerformerInfo performerInfo = keyValuePair.Value;

                if (performerInfo == null)
                {
                    continue;
                }

                float previousElapsedTime = performerInfo.ElapseTime;
                performerInfo.ElapseTime = Mathf.Min(performerInfo.ElapseTime + Time.deltaTime, Duration);
                bool isFinalTick = performerInfo.ElapseTime >= Duration;
                bool continueInteraction = false;

                if (_statChanges.Length > 0 && ((InteractionType == InteractionType.OverTime) || (InteractionType == InteractionType.AfterTime && isFinalTick)))
                {
                    continueInteraction = ApplyInteractionEffects(performer, (performerInfo.ElapseTime - previousElapsedTime) / Duration, isFinalTick);
                }

                if (!continueInteraction || isFinalTick)
                {
                    OnInteractionCompleted(performer, performerInfo.onCompleted);
                }
            }

            foreach (var performer in performersToCleanup)
            {
                currentPerformers.Remove(performer);
            }

            performersToCleanup.Clear();
        }
    }
}