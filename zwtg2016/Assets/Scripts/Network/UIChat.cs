using ExitGames.Client.Photon.Chat;
using System;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using AuthenticationValues = ExitGames.Client.Photon.Chat.AuthenticationValues;

namespace UI
{
	public class UIChat : MonoBehaviour, IChatClientListener
    {
	    public RectTransform m_mainChatPanel;
	    public InputField m_messageInput;
	    public Button m_sendMessage;
	    public ScrollRect m_incommingBox;
        private string m_appId = "9ded5b30-b6b5-48f0-9eeb-efa8944a324d";
        private ExitGames.Client.Photon.Chat.AuthenticationValues m_authenticationValues;
	    private ChatClient m_chatClient;
	    public GameObject m_messagePrefab;




        public void Start()
	    {
            ExitGames.Client.Photon.ConnectionProtocol connectProtocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
            m_chatClient = new ChatClient(this, connectProtocol);
            m_chatClient.ChatRegion = "EU";

            ExitGames.Client.Photon.Chat.AuthenticationValues authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();
            authValues.UserId = "Test";
            authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;
            m_chatClient.Connect(m_appId, "0.01", authValues);

        }

        void Update()
        {
            if (m_chatClient != null)
            {
                m_chatClient.Service();
            }
        }

	    public void LateUpdate()
	    {
	    }

	    void OnApplicationQuit()
        {
            if (m_chatClient != null) { m_chatClient.Disconnect(); }
        }

        public void SendMessage(string msgToSend)
        {
            if (m_messageInput != null && !m_messageInput.text.IsNullOrEmpty())
            {
                string l_inputFromTextField = m_messageInput.text;
                m_chatClient.PublishMessage("channelNameHere", l_inputFromTextField);
                GenerateTextBox();
            }
        }

        public void GenerateTextBox()
        {
            GameObject l_newMessage = Instantiate(m_messagePrefab);
            l_newMessage.transform.SetParent(m_incommingBox.transform);
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

	    public void OnConnected()
	    {
            m_chatClient.Subscribe(new string[] { "channelNameHere", "All" });
        }

	    public void OnChatStateChange(ChatState state)
	    {
	        print(state.ToString());
	    }
        
        public void OnGetMessages(string channelName, string[] senders, object[] messages)
	    {

	        int msgCount = messages.Length;
	        print("Got " + msgCount + " new messages!");

	        for (int i = 0; i < msgCount; i++)
	        {
	            string sender = senders[i];
	            string msg = messages[i].ToString();

	            print(sender + ": " + msg);

	            m_messagePrefab.GetComponentInChildren(typeof (Text)).GetComponent<Text>().text = sender + ": " + msg;
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

