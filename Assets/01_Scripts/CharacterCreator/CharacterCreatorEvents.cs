using System;
using UnityEngine;

namespace CharacterCreator
{
    public class CharacterCreatorEvents : MonoBehaviour
    {
        public static CharacterCreatorEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action<string> onDetailsButtonPressed;

        public void DetailsButtonPressed(string id)
        {
            if (onDetailsButtonPressed != null)
            {
                onDetailsButtonPressed(id);
            }
        }
    }
}
