using System.Collections.Generic;
using UnityEngine;

namespace SmartInteractions
{
    public class SmartInteractionManager : MonoBehaviour
    {
        public static SmartInteractionManager Instance { get; private set; } = null;
        public List<SmartInteraction> RegisteredObjects { get; private set; } = new List<SmartInteraction>();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Trying to create second SmartInteractionManager on {gameObject.name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void RegisterSmartInteraction(SmartInteraction toRegister)
        {
            RegisteredObjects.Add(toRegister);
        }

        public void DeregisterSmartInteraction(SmartInteraction toDeregister)
        {
            RegisteredObjects.Remove(toDeregister);
        }
    }
}