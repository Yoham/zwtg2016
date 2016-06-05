using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "0.1";
	const string ROOM_NAME = "lobby";

	public GameObject spawnPoint;
	public GameObject prefab;

	void Awake () {
		Debug.Log ("Awake");
//		PhotonNetwork.offlineMode = true;
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings(VERSION);
		}

		PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));

	}
		
	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 20 };

		// NOT THREAD SAFE!!
//		PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate(prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);

		PhotonNetwork.player.SetTeam (PunTeams.Team.psycho);
	}

	void OnGUI()
	{
		Debug.Log ("OnGUI");
		if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

		if (GUILayout.Button("Leave Room"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}



}

