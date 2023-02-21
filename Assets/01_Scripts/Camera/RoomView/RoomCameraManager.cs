using UnityEngine;

namespace Apartments
{
    public class RoomCameraManager : MonoBehaviour
    {
        [SerializeField] Vector3 _topViewPosition;
        [SerializeField] Vector3 _frontViewPosition;
        [SerializeField] float _movementTime;

        Vector3 _currentPosition;
        Vector3 _topRotation = new Vector3(90f, 0f, 0f);

        public void MoveToPreset(CameraPresets preset)
        {
            switch (preset)
            {
                case CameraPresets.Top:
                    LeanTween.move(gameObject, _topViewPosition, _movementTime);
                    LeanTween.rotate(gameObject, _topRotation, _movementTime);
                    break;
                case CameraPresets.Front:
                    LeanTween.move(gameObject, _frontViewPosition, _movementTime);
                    LeanTween.rotate(gameObject, Vector3.zero, _movementTime);
                    break;
                default:
                    break;
            }
        }

    }
}
