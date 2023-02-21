using System.Collections.Generic;
using UnityEngine;

namespace Blackboards
{
    public class BlackboardManager : MonoBehaviour
    {
        public static BlackboardManager Instance { get; private set; } = null;

        Dictionary<MonoBehaviour, Blackboard> individualBlackboards = new Dictionary<MonoBehaviour, Blackboard>();
        Dictionary<int, Blackboard> sharedBlackboards = new Dictionary<int, Blackboard>();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Trying to create second BlackboardManager on {gameObject.name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public Blackboard GetIndividualBlackboard(MonoBehaviour requestor)
        {
            if (!individualBlackboards.ContainsKey(requestor))
            {
                individualBlackboards[requestor] = new Blackboard();
            }

            return individualBlackboards[requestor];
        }

        public Blackboard GetSharedBlackboard(int id)
        {
            if (!sharedBlackboards.ContainsKey(id))
            {
                sharedBlackboards[id] = new Blackboard();
            }

            return sharedBlackboards[id];
        }
    }
}