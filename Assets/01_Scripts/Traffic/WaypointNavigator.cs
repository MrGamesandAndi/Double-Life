using UnityEngine;

namespace TrafficSystem
{
    public class WaypointNavigator : MonoBehaviour
    {
        CharacterNavigationManager _manager;
        int _direction;

        public Waypoint currentWaypoint;

        private void Awake()
        {
            _manager = GetComponent<CharacterNavigationManager>();
        }

        private void Start()
        {
            _direction = Mathf.RoundToInt(Random.Range(0f, 1f));
            _manager.SetDestination(currentWaypoint.GetPosition());
        }

        private void Update()
        {
            if (_manager.ReachedDestination)
            {
                bool shouldBranch = false;

                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                }

                if (shouldBranch)
                {
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
                }
                else
                {
                    if (_direction == 0)
                    {
                        if (currentWaypoint.nextWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                            _direction = 1;
                        }
                    }
                    else if (_direction == 1)
                    {
                        if (currentWaypoint.previousWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                            _direction = 0;
                        }
                    }
                }

                _manager.SetDestination(currentWaypoint.GetPosition());
            }
        }
    }
}
