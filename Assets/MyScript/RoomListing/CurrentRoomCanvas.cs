using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {
    
    public void OnClickStartSync()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClickStartDelayed()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
