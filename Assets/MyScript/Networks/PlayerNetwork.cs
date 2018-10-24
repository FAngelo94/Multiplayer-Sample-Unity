using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }

    private PhotonView photonView;

    private int PlayersInGame = 0;

    private PlayerMovement currentPlayer;

	// Use this for initialization
	private void Awake () {
        instance = this;
        PlayerName = "Angelo#" + Random.Range(1000, 9999);
        photonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

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
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NotMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient,PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayerManagement.instance.AddPlayerStats(photonPlayer);

        PlayersInGame++;
        if(PlayersInGame==PhotonNetwork.playerList.Length)
        {
            print("All players are in the game");
            photonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    public void NewHealth(PhotonPlayer photonPlayer, int health)
    {
        photonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_NewHealth(int health)
    {
        if (currentPlayer == null)
            return;

        if (health <= 0)
            PhotonNetwork.Destroy(currentPlayer.gameObject);
        else
            currentPlayer.health = health;
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomValue = Random.Range(0f, 5f);
        GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefab", "NewPlayer"), Vector3.up * randomValue, Quaternion.identity, 0);
        currentPlayer = obj.GetComponent<PlayerMovement>();
    }
}
