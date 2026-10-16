using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatUI : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField addressInput;
    [SerializeField] private TMP_InputField portInput;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Image statusIcon;

    [Header("Chat")]
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private GameObject chatMessageGO;
    [SerializeField] private GameObject chatContentDisplay;
    [SerializeField] private ScrollRect chatScrollRect;

    public string Username => usernameInput.text;
    public string Address => addressInput.text;
    public string Message => messageInput.text;

    // Parses the port input field as a ushort, defaulting to 7777 if invalid
    public ushort Port
    {
        get
        {
            if (ushort.TryParse(portInput.text, out ushort port))
            {
                return port;
            }

            return 7777;
        }
    }

    // Sets the status text to the given string
    public void SetStatus(string status, Color color)
    {
        statusText.text = status;
        statusIcon.color = color;
    }

    // Adds a message to the chat display
    public void AddMessage(string username, string message)
    {
        var newMessage = Instantiate(chatMessageGO, chatContentDisplay.transform);

        newMessage.TryGetComponent(out TextMeshProUGUI messageText);
        messageText.text = $"<color=yellow><b>{username}:</b></color> {message}";

        StartCoroutine(ScrollToBottom());
    }

    // Clears the message input field
    public void ClearMessage()
    {
        messageInput.text = "";
        messageInput.ActivateInputField();
    }

    private IEnumerator ScrollToBottom()
    {
        // Wait until the end of the frame so Unity can update the UI layout.
        yield return new WaitForEndOfFrame();

        // Force Unity to recalculate the UI layout.
        Canvas.ForceUpdateCanvases();

        // Scroll to the bottom.
        chatScrollRect.verticalNormalizedPosition = 0f;
    }
}
