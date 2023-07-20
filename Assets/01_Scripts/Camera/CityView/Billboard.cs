using UnityEngine;

namespace CameraSystem.CityView
{
    public class Billboard : MonoBehaviour
	{
		Camera _camera;

		[SerializeField] bool _useStaticBillboard;

		private void Awake()
		{
			_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}

		private void LateUpdate()
		{
			if (!_useStaticBillboard)
			{
				transform.LookAt(_camera.transform);
			}
			else
			{
				transform.rotation = _camera.transform.rotation;
			}

			transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
		}
	}
}