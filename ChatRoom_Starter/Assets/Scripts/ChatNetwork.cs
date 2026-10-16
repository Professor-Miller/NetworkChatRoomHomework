using Unity.Netcode;
using UnityEngine;

public class ChatNetwork : NetworkBehaviour
{
    [SerializeField] private ChatUI chatUI;

    // CLIENT: Request to send a message
    public void SendMessage()
    {
        // TODO 1:
        // Get the message from the ChatUI.

        // TODO 2:
        // Check if the message is empty or whitespace.
        // If it is, do not send anything.

        // TODO 3:
        // Request that the server send the message
        // using the appropriate RPC.

        // TODO 4:
        // Clear the message input field after
        // submitting the request.
    }

    // SERVER: Validate and authorize the message
    [Rpc(SendTo.Server)]
    private void SendMessageRpc(string message, RpcParams rpcParams = default)
    {
        // TODO 5:
        // Identify the ClientId of the player
        // who sent this RPC request.
        // HINT: Look into RpcParams.

        // TODO 6:
        // SERVER VALIDATION:
        // Reject messages that are empty or
        // contain only whitespace.

        // TODO 7:
        // SERVER VALIDATION:
        // Reject messages longer than 100 characters.

        // TODO 8:
        // SERVER AUTHORITY:
        // Generate a username using the sender's ClientId.
        // FORMAT: "Player {ClientId}"

        // TODO 9:
        // SERVER BROADCAST:
        // If the message passed all validation,
        // send the approved username and message
        // to everyone using the appropriate RPC.
    }

    // EVERYONE: Display approved messages
    [Rpc(SendTo.Everyone)]
    private void ReceiveMessageRpc(string username, string message)
    {
        // TODO 10:
        // Display the approved message in the ChatUI.
        // HINT: Look at the methods in ChatUI.
    }
}
