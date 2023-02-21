using UnityEngine.Events;

namespace SmartInteractions
{
    public class PerformerInfo
    {
        public float ElapseTime;
        public UnityAction<BaseInteraction> onCompleted;
    }
}
