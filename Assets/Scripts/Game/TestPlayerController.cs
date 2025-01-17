using Photon.Pun;
using TMPro;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
	[SerializeField] private TextMeshPro nickNameText;

	private PhotonView _photonView;
	
	private Material[] materials;
    
	private bool movingRight = true;
	private float initialX;

	private void Awake()
	{
		_photonView = GetComponent<PhotonView>();
	}

	private void Start()
	{
		nickNameText.text = _photonView.Controller.NickName;
		
		initialX = transform.position.x;
	}
	
	private void Update()
	{
		if (_photonView.IsMine == true)
		{
			// Your Input
			float targetX = movingRight ? initialX + 2f : initialX - 2f;
			
			transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetX, 2f * Time.deltaTime), 
				transform.position.y, 
				transform.position.z);
			
			if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
			{
				movingRight = !movingRight;
			}
		}
	}
}