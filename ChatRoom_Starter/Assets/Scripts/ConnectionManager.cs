using Unity.Netcode.Transports.UTP;
using UnityEngine;

using NetManager = Unity.Netcode.NetworkManager;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private ChatUI chatUI;

    void Start()
    {
        // Subscribe to connection callbacks
        NetManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        chatUI.SetStatus("Disconnected", Color.red);
    }

    void OnDestroy()
    {
        // If there is no NetworkManager, there are no callbacks to clean up
        if (NetManager.Singleton == null) return;

        // Unsubscribe from connection callbacks
        NetManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    // Host the game on the local machine
    public void Host()
    {
        // Get the UnityTransport component from the NetworkManager
        var transport = NetManager.Singleton.GetComponent<UnityTransport>();

        // Set the connection data to the local machine's IP and the chat UI's port
        transport.SetConnectionData("0.0.0.0", chatUI.Port);

        // Start the host
        bool started = NetManager.Singleton.StartHost();

        // If the host started successfully, set the status to "Hosting on port {chatUI.Port}"
        if (started)
        {
            chatUI.SetStatus($"Hosting on port {chatUI.Port}", Color.green);
        }

        // Otherwise, set the status to "Failed to start host"
        else
        {
            chatUI.SetStatus("Failed to start host", Color.yellow);
        }
    }

    // Join the game on the local machine
    public void Join()
    {
        // Get the UnityTransport component from the NetworkManager
        var transport = NetManager.Singleton.GetComponent<UnityTransport>();

        // Set the connection data to the chat UI's address and port
        transport.SetConnectionData(chatUI.Address, chatUI.Port);

        // Set the status to "Connecting to {chatUI.Address}:{chatUI.Port}..."
        chatUI.SetStatus($"Connecting to {chatUI.Address}:{chatUI.Port}...", Color.green);

        // Start the client
        bool started = NetManager.Singleton.StartClient();

        // If the client failed to start, set the status to "Failed to start client"
        if (!started)
        {
            chatUI.SetStatus("Failed to start client", Color.yellow);
        }
    }

    // Called when the client successfully connects to the server
    private void OnClientConnected(ulong clientId)
    {
        // If the client is the local client, set the status to "Hosting" or "Connected"
        if (clientId == NetManager.Singleton.LocalClientId)
        {
            if (NetManager.Singleton.IsHost)
            {
                chatUI.SetStatus("Hosting", Color.green);
            }
            else
            {
                chatUI.SetStatus("Connected", Color.green);
            }
        }
    }

    // Called when the client disconnects from the server
    private void OnClientDisconnected(ulong clientId)
    {
        if (clientId == NetManager.Singleton.LocalClientId)
        {
            chatUI.SetStatus("Disconnected", Color.red);
        }
    }
}
