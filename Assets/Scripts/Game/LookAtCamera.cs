using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Camera targetCamera;
	
	private void Start()
	{
		targetCamera = Camera.main;
	}

	private void Update()
	{
		if (targetCamera != null)
		{
			Vector3 direction = targetCamera.transform.position - transform.position;
			direction.y = 0;
			
			transform.rotation = Quaternion.LookRotation(-direction);
		}
	}
}