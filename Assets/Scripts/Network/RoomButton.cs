using TMPro;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;
	
	public void JoinRoom()
	{
		FindObjectOfType<CreateAndJoin>().JoinRoomInList(text.text);
	}

	public void SetRoomName(string roomName)
	{
		text.text = roomName;
	}
}