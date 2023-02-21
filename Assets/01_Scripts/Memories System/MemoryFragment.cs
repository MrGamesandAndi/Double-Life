using Localisation;
using SmartInteractions;
using System;
using UnityEngine;

namespace MemorySystem
{
    [CreateAssetMenu(menuName = "AI/Memory", fileName = "Memory_")]
    public class MemoryFragment : ScriptableObject
    {
        public LocalisedString Name;
        public LocalisedString Description;
        public float Duration = 0f;
        public InteractionStatChange[] StatChanges;
        public MemoryFragment[] MemoriesCountered;

        public int Occurrences { get; private set; } = 0;
        public float DurationRemaining { get; private set; } = 0f;

        public bool IsSimilarTo(MemoryFragment other)
        {
            return Name.Value == other.Name.Value && Description.Value == other.Description.Value;
        }

        public bool IsCancelledBy(MemoryFragment other)
        {
            foreach (var fragment in MemoriesCountered)
            {
                if (fragment.IsSimilarTo(other))
                {
                    return true;
                }
            }

            return false;
        }

        public void Reinforce(MemoryFragment other)
        {
            DurationRemaining = Mathf.Max(DurationRemaining, other.DurationRemaining);
            Occurrences++;
        }

        public MemoryFragment Duplicate()
        {
            var newMemory = Instantiate(this);
            newMemory.Occurrences = 1;
            newMemory.DurationRemaining = Duration;
            return newMemory;
        }

        public bool Tick(float deltaTime)
        {
            DurationRemaining -= deltaTime;
            return DurationRemaining > 0;
        }
    }
}
