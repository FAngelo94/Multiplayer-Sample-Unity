using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {

    public static PlayerManagement instance;
    private PhotonView photonView;

    private List<PlayerStats> playerStats = new List<PlayerStats>();

    private void Awake()
    {
        instance = this;
        photonView = GetComponent<PhotonView>();
    }
	
    public void AddPlayerStats(PhotonPlayer photonPlayer)
    {
        int index = playerStats.FindIndex(x => x.photonPlayer == photonPlayer);
        if (index == -1)
        {
            playerStats.Add(new PlayerStats(photonPlayer, 30));
        }
    }

    public void ModifyHealth(PhotonPlayer photonPlayer, int value)
    {
        int index = playerStats.FindIndex(x => x.photonPlayer == photonPlayer);
        if (index != -1)
        {
            PlayerStats tmpPlayerStats = playerStats[index];
            tmpPlayerStats.health += value;
            PlayerNetwork.instance.NewHealth(photonPlayer, tmpPlayerStats.health);
        }
    }
}

public class PlayerStats
{
    public PlayerStats(PhotonPlayer photonPlayer, int health)
    {
        this.photonPlayer = photonPlayer;
        this.health = health;
    }

    public readonly PhotonPlayer photonPlayer;
    public int health;
}
