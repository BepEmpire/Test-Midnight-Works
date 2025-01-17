using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Camera _targetCamera;
	
	private void Start()
	{
		_targetCamera = Camera.main;
	}

	private void Update()
	{
		if (_targetCamera != null)
		{
			Vector3 direction = _targetCamera.transform.position - transform.position;
			direction.y = 0;
			
			transform.rotation = Quaternion.LookRotation(-direction);
		}
	}
}