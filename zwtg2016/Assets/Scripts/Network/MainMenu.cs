using UnityEngine;
using System.Collections;
using UI;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	const string VERSION = "0.1";

    public InputField m_roomName;
    public InputField m_joinRoomName;
    public InputField m_playerName;
    public ToggleGroup m_toggleGroup;
    public Toggle m_psycho;
    public Toggle m_doc;
    public GameObject m_menu;
    public GameObject m_chat;

	private string roomName = "myRoom";

	private bool psycho = true;
	private bool doctor = false;

    public void JoinRoom()
    {
        PhotonNetwork.playerName = m_playerName.text;
        PhotonNetwork.player.SetTeam(getTeam());
        PhotonNetwork.JoinRoom(m_joinRoomName.text);
    }

    public void CreateRoom()
    {
        PhotonNetwork.playerName = m_playerName.text;
        PhotonNetwork.player.SetTeam(getTeam());
        PhotonNetwork.CreateRoom(m_roomName.text, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
    }

    public PunTeams.Team getTeam()
    {

        return m_psycho.isOn ? PunTeams.Team.psycho : PunTeams.Team.doctor;
    }


    public void SwitchGUIState()
    {
        if (m_menu.activeSelf)
        {
            m_menu.SetActive(false);
            m_chat.SetActive(true);
        }
        else
        {
            m_menu.SetActive(true);
            m_chat.SetActive(false);

        }

    }


    public void Start()
    {
        m_roomName.text = roomName;
        m_joinRoomName.text = roomName;
        m_playerName.text = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));
        PhotonNetwork.playerName = m_playerName.text;
        //m_toggleGroup.
    }

    private void LateUpdate()
    {
        if (!PhotonNetwork.connected)
        {
            Debug.Log("Wait for a connection");
            return;   //Wait for a connection
        }
        if (PhotonNetwork.room != null)
        {
            return; //Only when we're not in a Room
        }
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            Debug.Log("..no games available..");
        }
        else {
            //Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                Debug.Log(game.name + " " + game.playerCount + "/" + game.maxPlayers);
                //if (GUILayout.Button("JOIN"))
                //{
                //    PhotonNetwork.JoinRoom(game.name);
                //}
            }
        }

    }


    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            Debug.Log("Wait for a connection");
            ShowConnectingGUI();
            return;   //Wait for a connection
        }
        
    }

    void ShowConnectingGUI()
    {
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
