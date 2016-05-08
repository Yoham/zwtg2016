using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "0.1";
	const string ROOM_NAME = "lobby";

	public GameObject spawnPoint;
	public GameObject prefab;

	void Start () {
		PhotonNetwork.ConnectUsingSettings(VERSION);
	}


	void OnJoinedLobby() {
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 4 };

		// NOT THREAD SAFE!!
		PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate(prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
	}
}
