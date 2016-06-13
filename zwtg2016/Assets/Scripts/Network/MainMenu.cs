using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	const string VERSION = "0.1";

	private string roomName = "myRoom";
	private Vector2 scrollPos = Vector2.zero;

	private bool psycho = true;
	private bool doctor = false;


	void OnGUI() {
		//Debug.Log ("OnGUI Menu");

		if (!PhotonNetwork.connected) {
			Debug.Log ("Wait for a connection");
			ShowConnectingGUI();
			return;   //Wait for a connection
		}

		if (PhotonNetwork.room != null) {
			return; //Only when we're not in a Room
		}

		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

		GUILayout.Label("Main Menu");

		GUILayout.Space(15);

		//Player name
		GUILayout.BeginHorizontal();
		GUILayout.Label("Player name:", GUILayout.Width(150));
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
		if (GUI.changed) { //Save name
			PlayerPrefs.SetString ("playerName", PhotonNetwork.playerName);
		}
		GUILayout.EndHorizontal();
		//Select side
		GUILayout.BeginHorizontal();
		GUILayout.Label("Side:", GUILayout.Width(150));
		psycho = GUILayout.Toggle(psycho, "PSYCHO");
		doctor = GUILayout.Toggle(doctor, "DOCTOR");

		GUILayout.EndHorizontal();

		GUILayout.Space(15);

		//Join room by title
		GUILayout.BeginHorizontal();
		GUILayout.Label("JOIN ROOM:", GUILayout.Width(150));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO")) {
			PhotonNetwork.JoinRoom(roomName);
		}
		GUILayout.EndHorizontal();

		//Create a room (fails if exist!)
		GUILayout.BeginHorizontal();
		GUILayout.Label("CREATE ROOM:", GUILayout.Width(150));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO")) {
			// using null as TypedLobby parameter will also use the default lobby
			PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(30);

		GUILayout.Label("ROOM LISTING:");
		if (PhotonNetwork.GetRoomList().Length == 0) {
			GUILayout.Label("..no games available..");
		} else {
			//Room listing: simply call GetRoomList: no need to fetch/poll whatever!
			scrollPos = GUILayout.BeginScrollView(scrollPos);
			foreach (RoomInfo game in PhotonNetwork.GetRoomList()) {
				GUILayout.BeginHorizontal();
				GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
				if (GUILayout.Button("JOIN")) {
					PhotonNetwork.JoinRoom(game.name);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}

		GUILayout.EndArea();
	}

	void ShowConnectingGUI() {
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

		GUILayout.Label("Connecting to Photon server.");
		GUILayout.Label("Hint: This demo uses a settings file and logs the server address to the console.");

		GUILayout.EndArea();
	}

	//
	//	public void OnConnectedToMaster() {
	//		// this method gets called by PUN, if "Auto Join Lobby" is off.
	//		// this demo needs to join the lobby, to show available rooms!
	//
	//		PhotonNetwork.JoinLobby();  // this joins the "default" lobby
	//	}
}
