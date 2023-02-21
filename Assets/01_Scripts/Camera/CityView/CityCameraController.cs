using UnityEngine;

namespace CameraSystem
{
    public class CityCameraController : MonoBehaviour
    {
        [SerializeField] float _keyboardSpeed;
        [SerializeField] float _dragSpeed;
        [SerializeField] float _screenEdgeSpeed;
        [SerializeField] float _screenEdgeBorderSize;
        [SerializeField] float _mouseRotationSpeed;
        [SerializeField] float _followMoveSpeed;
        [SerializeField] float _followRotationSpeed;
        [SerializeField] float _minHeight;
        [SerializeField] float _maxHeight;
        [SerializeField] float _mapLimitSmoothing;
        [SerializeField] Vector2 _mapLimits;
        [SerializeField] Vector2 _rotationLimits;
        [SerializeField] Vector3 _followOffset;

        Transform _targetToFollow;
        float _yaw;
        float _pitch;
        KeyCode _dragKey = KeyCode.Mouse1;
        KeyCode _rotationKey = KeyCode.Mouse2;
        LayerMask _groundMask;

        private void Start()
        {
            _groundMask = LayerMask.NameToLayer(Layers.Ground);
            _pitch = transform.eulerAngles.x;
        }

        private void Update()
        {
            if (!_targetToFollow)
            {
                Move();
            }
            else
            {
                FollowTarget();
            }

            Rotation();
            LimitPosition();
        }

        private void Move()
        {
            if (Input.GetKey(_dragKey))
            {
                Vector3 desiredDragMove = new Vector3(-Input.GetAxis("Mouse X"), 0f, -Input.GetAxis("Mouse Y")) * _dragSpeed;
                desiredDragMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredDragMove * Time.deltaTime;
                desiredDragMove = transform.InverseTransformDirection(desiredDragMove);
                transform.Translate(desiredDragMove, Space.Self);
            }
            else
            {
                Vector3 desiredMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                desiredMove *= _keyboardSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = transform.InverseTransformDirection(desiredMove);
                transform.Translate(desiredMove, Space.Self);
            }
        }

        private void Rotation()
        {
            if (Input.GetKey(_rotationKey))
            {
                _yaw += _mouseRotationSpeed * Input.GetAxis("Mouse X");
                _pitch -= _mouseRotationSpeed * Input.GetAxis("Mouse Y");
                _pitch = Mathf.Clamp(_pitch, _rotationLimits.x, _rotationLimits.y);
                transform.eulerAngles = new Vector3(_pitch, _yaw, 0f);
            }
        }

        private void LimitPosition()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(
                Mathf.Clamp(transform.position.x, -_mapLimits.x, _mapLimits.x), transform.position.y,
                Mathf.Clamp(transform.position.z, -_mapLimits.y, _mapLimits.y)), Time.deltaTime * _mapLimitSmoothing);
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = new Vector3(_targetToFollow.position.x, transform.position.y, _targetToFollow.position.z) + _followOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _followMoveSpeed);

            if (_followRotationSpeed > 0 && !Input.GetKey(_rotationKey))
            {
                Vector3 targetDirection = (_targetToFollow.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection), _followRotationSpeed * Time.deltaTime);
                transform.rotation = targetRotation;
                _pitch = transform.eulerAngles.x;
                _yaw = transform.eulerAngles.y;
            }
        }

        public void SetTarget(Transform target)
        {
            _targetToFollow = target;
        }

        public void ResetTarget()
        {
            _targetToFollow = null;
        }
    }
}