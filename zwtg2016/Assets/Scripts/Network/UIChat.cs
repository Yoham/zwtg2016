using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace UI
{
    public class UIChat : MonoBehaviour, IChatClientListener
    {
        public RectTransform m_mainChatPanel;
        public InputField m_messageInput;
        public Button m_sendMessage;
        public RectTransform m_incommingBox;
        public RectTransform m_incommingBoxTeam;
        public RectTransform m_teamPanel;
        public RectTransform m_allPanel;

        private string m_appId = "9ded5b30-b6b5-48f0-9eeb-efa8944a324d"; //Drugi klucz do PhotonApi - Jakub
        private ExitGames.Client.Photon.Chat.AuthenticationValues m_authenticationValues;
        private ChatClient m_chatClient;
        public GameObject m_messagePrefab;
        public KeyCode toggleKey = KeyCode.BackQuote;
        public KeyCode chatSwitchKey = KeyCode.Tab;
        public PunTeams.Team teamChannelName = PunTeams.Team.psycho;

        public bool m_chatTabState = true;

        public void Start()
        {
            ExitGames.Client.Photon.ConnectionProtocol connectProtocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
            m_chatClient = new ChatClient(this, connectProtocol);
            m_chatClient.ChatRegion = "EU";
        }

        public void Authenticate(string p_user,PunTeams.Team side= PunTeams.Team.psycho)
        {
            teamChannelName = side;
            m_authenticationValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();
            //m_authenticationValues.UserId = System.Environment.UserName;
            m_authenticationValues.UserId = p_user;
            m_authenticationValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;
            m_chatClient.Connect(m_appId, "0.01", m_authenticationValues);
        }

        private void Update()
        {
            if (m_chatClient != null)
            {
                m_chatClient.Service();
            }

            if (Input.GetKeyDown(toggleKey))
            {
                SendMessage("");
                m_messageInput.Select();
            }
            if (Input.GetKeyDown(chatSwitchKey))
            {
                SwitchgChatTabs();
            }
        }

        public void LateUpdate()
        {
        }

        public void SwitchgChatTabs()
        {
            if(m_chatTabState)
            {
                m_allPanel.gameObject.SetActive(false);
                m_teamPanel.gameObject.SetActive(true);
                m_chatTabState = false;
            }
            else
            {
                m_allPanel.gameObject.SetActive(true);
                m_teamPanel.gameObject.SetActive(false);
                m_chatTabState = true;
            }


        }

        public void SwitchGlobalTab()
        {
            if (!m_chatTabState)
            {
                m_allPanel.gameObject.SetActive(true);
                m_teamPanel.gameObject.SetActive(false);
                m_chatTabState = true;
            }
            
        }

        public void SwitchTeamTab()
        {
            if (m_chatTabState)
            {
                m_allPanel.gameObject.SetActive(false);
                m_teamPanel.gameObject.SetActive(true);
                m_chatTabState = false;
            }

        }

        private void OnApplicationQuit()
        {
            if (m_chatClient != null) { m_chatClient.Disconnect(); }
        }

        public void SendMessage(string p_message)
        {
            if (m_messageInput != null && !m_messageInput.text.IsNullOrEmpty())
            {
                string l_inputFromTextField = m_messageInput.text;
                m_messageInput.text = "";
                m_chatClient.PublishMessage(getCurrentChanel(), l_inputFromTextField);
                
            }
        }

        public string getCurrentTime()
        {
            return "["+System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute+"]";
        }

        public void GenerateTextBox(string p_message)
        {
            GameObject l_newMessage = Instantiate(m_messagePrefab);
            RectTransform currentPanel = getCurrentMsgPanel();
            l_newMessage.transform.SetParent(currentPanel.transform);
            currentPanel.sizeDelta += new Vector2(0, 30);
            l_newMessage.GetComponentInChildren<Text>().text = getCurrentTime() + " "+p_message;
        }

        public void GenerateTextBox(string p_message, RectTransform panel)
        {
            GameObject l_newMessage = Instantiate(m_messagePrefab);
            l_newMessage.transform.SetParent(panel.transform);
            panel.sizeDelta += new Vector2(0, 30);
            l_newMessage.GetComponentInChildren<Text>().text = getCurrentTime() + " " + p_message;
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            print(level.ToString() + " " + message);
        }

        public void OnDisconnected()
        {
            print("Disconnected");
            m_chatClient.Disconnect();
        }

        public string getCurrentChanel()
        {
            return m_chatTabState ? "All" : teamChannelName.ToString();
        }

        public RectTransform getCurrentMsgPanel()
        {
            return m_chatTabState ? m_incommingBox : m_incommingBoxTeam;
        }

        public void OnConnected()
        {
            m_chatClient.Subscribe(new string[] { "All", teamChannelName.ToString() });
        }

        public void OnChatStateChange(ChatState state)
        {
            print(state.ToString());
        }

        private RectTransform parseChanellName(string channel)
        {
            return channel.Equals("All") ? m_incommingBox :m_incommingBoxTeam;
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            int msgCount = messages.Length;
            //print("Got " + msgCount + " new messages!");

            for (int i = 0; i < msgCount; i++)
            {
                string sender = senders[i];
                string msg = messages[i].ToString();

                //print(sender + ": " + msg);

                GenerateTextBox(sender + ": " + msg, parseChanellName(channelName));
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            print("Private!");
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            print("Subscribed to a new channel!");
            print(channels[0].ToString() + " " + results[0].ToString());
        }

        public void OnUnsubscribed(string[] channels)
        {
            print("UnSubscribed from a new channel! " + channels[0].ToString());
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            print(user);
            print(status);
            print(gotMessage);
            print(message.ToString());
        }
    }
}