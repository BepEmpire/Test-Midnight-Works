using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomsList : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform contentTransform;
    [SerializeField] private RoomButton roomButtonPrefab;
    [SerializeField] private RoomButton[] roomsArray;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        DeleteCurrentRooms();

        roomsArray = new RoomButton[roomList.Count];

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                RoomButton currentRoomButton = Instantiate(roomButtonPrefab, contentTransform);
                currentRoomButton.SetRoomName(roomList[i].Name);
                roomsArray[i] = currentRoomButton;
            }
        }
    }

    private void DeleteCurrentRooms()
    {
        foreach (var room in roomsArray)
        {
            if (room != null)
            {
                Destroy(room.gameObject);
            }
        }
    }
}