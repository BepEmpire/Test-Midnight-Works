using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
	[SerializeField] private UnityEvent onJoinedLobby;
	
	private void Start()
	{
		Debug.Log("Start connection to server");
		
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connect to server");
		
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("Join lobby scene");
		
		InitUserName();
		onJoinedLobby?.Invoke();
	}

	private void InitUserName()
	{
		if (!PlayerPrefs.HasKey(Keys.PLAYER_NAME))
		{
			PlayerPrefs.SetString(Keys.PLAYER_NAME, GetRandomName());
		}

		PhotonNetwork.NickName = PlayerPrefs.GetString(Keys.PLAYER_NAME, GetRandomName());
	}

	private string GetRandomName()
	{
		return "User" + Random.Range(0, 1000);
	}
}