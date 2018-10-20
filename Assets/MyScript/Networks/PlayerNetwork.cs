using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }

    private PhotonView photonView;

    private int PlayersInGame = 0;

	// Use this for initialization
	private void Awake () {
        instance = this;
        PlayerName = "Angelo#" + Random.Range(1000, 9999);
        photonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneFinishLoading;//call the method when the scene is loaded
	}

    private void OnSceneFinishLoading(Scene scene,LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NotMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        PlayersInGame = 1;
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NotMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if(PlayersInGame==PhotonNetwork.playerList.Length)
        {
            print("All players are in the game");
        }
    }
}
