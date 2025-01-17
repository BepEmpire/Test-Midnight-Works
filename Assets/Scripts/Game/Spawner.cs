using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Transform[] positions;

	private void Start()
	{
		GameObject player = PhotonNetwork.Instantiate("Player", GetRandomPosition(), Quaternion.identity);
	}

	private Vector3 GetRandomPosition()
	{
		return positions[Random.Range(0, positions.Length)].position;
	}
}