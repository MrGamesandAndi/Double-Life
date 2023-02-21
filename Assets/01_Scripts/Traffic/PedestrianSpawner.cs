using General;
using SaveSystem;
using System.Collections;
using UnityEngine;

namespace TrafficSystem
{
    public class PedestrianSpawner : MonoBehaviour
    {
        public static PedestrianSpawner Instance;

        [SerializeField] GameObject _pedestrianPrefab;
        [SerializeField] int _pedestriansToSpawn;
        [SerializeField] GameObject _parentGO;

        float _progress;
        bool _isDone;

        public bool IsDone { get => _isDone; set => _isDone = value; }
        public float Progress { get => _progress; set => _progress = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _pedestriansToSpawn = PopulationManager.Instance.DoublesList.Count;
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            int count = 0;

            while (count < _pedestriansToSpawn)
            {
                GameObject obj = Instantiate(_pedestrianPrefab);
                Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
                obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
                obj.transform.position = child.position;
                obj.transform.parent = _parentGO.transform;
                yield return new WaitForEndOfFrame();
                _progress = ((float)count / (float)_pedestriansToSpawn);
                count++;
            }

            yield return new WaitForSeconds(3f);
            _isDone = true;
        }
    }
}