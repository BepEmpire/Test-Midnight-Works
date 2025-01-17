using Photon.Pun;
using TMPro;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
	[SerializeField] private TMP_InputField inputCreate;

	public void CreateRoom()
	{
		Debug.Log($"Create Room: {inputCreate.text}");
		
		PhotonNetwork.CreateRoom(inputCreate.text);
	}

	public void JoinRoomInList(string roomName)
	{
		Debug.Log($"Join Room: {roomName}");
		
		PhotonNetwork.JoinRoom(roomName);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel(Scenes.MultiplayerScene.ToString());
	}
}