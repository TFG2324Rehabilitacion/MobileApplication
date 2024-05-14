using Riptide;
using System;
using UnityEngine;

public enum MessageID
{
    orientation = 1
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance
    {

        get { return instance; }
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
            else if (instance != value)
            {
                Destroy(value);
            }
        }
    }

    public Client Client { get; private set; }

    [SerializeField] private ushort port;
    private MessageSender messageSender;

    private void Awake()
    {
        Instance = this;
        messageSender = GetComponent<MessageSender>();
        messageSender.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Client = new Client();
        Client.Connected += OnConnectionMade;
        Client.ConnectionFailed += OnConnectionFailed;
        Client.Disconnected += OnClientDisconnected;
    }

    // Update is called once per frame
    void Update()
    {
        Client.Update();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect(string ip)
    {
        if (Client.Connect($"{ip}:{port}"))
        {
            //messageSender.enabled = true;
        }
        else
        {
            Debug.Log("Connect failed");
            UIManager.Singleton.ConnectFailed();
        }
    }

    private void OnConnectionMade(object sender, EventArgs e)
    {
        Debug.Log("Client connected");
        messageSender.enabled = true;
    }

    private void OnConnectionFailed(object sender, EventArgs e)
    {
        UIManager.Singleton.ConnectFailed();
        messageSender.enabled = false;
    }

    private void OnClientDisconnected(object sender, EventArgs e)
    {
        UIManager.Singleton.ConnectFailed();
        messageSender.enabled = false;
    }

    public void SendMessageToServer(Vector3 orientation)
    {
        Message message = Message.Create(MessageSendMode.Unreliable, (ushort)MessageID.orientation);
        message.AddVector3(orientation);
        Client.Send(message);
    }
}
