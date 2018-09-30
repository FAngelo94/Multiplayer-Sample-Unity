using UnityEngine.UI;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;

    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    public string RoomName { get; private set; }
    public bool Updated { get; set; }

    private void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.instane.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;
        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponentInChildren<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        Debug.Log("NameRoom=" + text);
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}
