using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }

	// Use this for initialization
	private void Awake () {
        instance = this;
        PlayerName = "Angelo#" + Random.Range(1000, 9999);
	}
	
}
