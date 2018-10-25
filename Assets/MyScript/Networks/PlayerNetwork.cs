using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }
    private PhotonView photonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Hashtable m_playerCustomProprieties = new ExitGames.Client.Photon.Hashtable();
    private PlayerMovement currentPlayer;
    private Coroutine m_pingCoroutine;

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
        PhotonNetwork.LoadLevel(2);
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
        PhotonNetwork.Instantiate(Path.Combine("Prefab", "NewPlayer"), Vector3.up * randomValue, Quaternion.identity, 0); 
    }

    private IEnumerator C_SetPing()

    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProprieties["Ping"] = PhotonNetwork.GetPing();
            //m_playerCustomProprieties.Add("GamesOne", 5); is equal to the row above
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProprieties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }

    //called by photon when connected to the master server
    private void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null)
            StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }
}
