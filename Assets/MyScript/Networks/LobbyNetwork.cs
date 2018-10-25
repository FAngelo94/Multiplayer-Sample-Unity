using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            print("Connecting to server...");
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
	}

    private void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.automaticallySyncScene = false;//true with sync
        PhotonNetwork.playerName = PlayerNetwork.instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinLobby()
    {
        print("Joined Lobby");
        if(!PhotonNetwork.inRoom)
            MainCanvasManager.instane.LobbyCanvas.transform.SetAsLastSibling();
    }
}
