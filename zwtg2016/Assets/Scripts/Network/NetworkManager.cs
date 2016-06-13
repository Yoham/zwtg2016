using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "0.1";
	const string ROOM_NAME = "lobby";

	public GameObject spawnPoint;
	public GameObject prefab;
    public GameObject standbyCamera;

    public float respawnTimer = 0f;

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
	}

	void OnJoinedRoom() {

		Debug.Log ("Working?");
		Debug.Log (prefab.name);

		PhotonNetwork.Instantiate(prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);

		PhotonNetwork.player.SetTeam (PunTeams.Team.psycho);
	}

	void OnGUI()
	{
		//Debug.Log ("OnGUI");
		if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

		if (GUILayout.Button("Leave Room"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}

    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                standbyCamera.SetActive(false);
                PhotonNetwork.Instantiate(prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
                PhotonNetwork.player.SetTeam(PunTeams.Team.psycho);
            }
        }
    }



}

