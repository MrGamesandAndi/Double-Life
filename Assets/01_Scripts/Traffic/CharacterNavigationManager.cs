using UnityEngine;

namespace TrafficSystem
{
    public class CharacterNavigationManager : MonoBehaviour
    {
        [SerializeField] float _movementSpeed = 1f;
        [SerializeField] float _rotationSpeed = 120f;
        [SerializeField] float _stopDistance = 2.5f;
        [SerializeField] Vector3 _destination;
        [SerializeField] bool _reachedDestination;

        public bool ReachedDestination { get => _reachedDestination; set => _reachedDestination = value; }

        private void Update()
        {
            if (transform.position != _destination)
            {
                Vector3 destinationDirection = _destination - transform.position;
                destinationDirection.y = 0;
                float destinationDistance = destinationDirection.magnitude;

                if (destinationDistance >= _stopDistance)
                {
                    _reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
                }
                else
                {
                    _reachedDestination = true;
                }
            }
            else
            {
                _reachedDestination = true;
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _reachedDestination = false;
        }
    }
}
