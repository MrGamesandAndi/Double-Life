using System.Collections.Generic;
using UnityEngine;

namespace SmartInteractions
{
    public class SmartInteraction : MonoBehaviour
    {
        [SerializeField] protected string _displayName;
        protected List<BaseInteraction> cachedInteractions = null;

        public string DisplayName => _displayName;
        public List<BaseInteraction> Interactions
        {
            get
            {
                if (cachedInteractions == null)
                {
                    cachedInteractions = new List<BaseInteraction>(GetComponents<BaseInteraction>());
                }

                return cachedInteractions;
            }
        }

        private void Start()
        {
            SmartInteractionManager.Instance.RegisterSmartInteraction(this);
        }

        private void OnDestroy()
        {
            SmartInteractionManager.Instance.DeregisterSmartInteraction(this);
        }
    }
}