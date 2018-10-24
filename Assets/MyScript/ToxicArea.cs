using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicArea : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonView photonView = other.GetComponent<PhotonView>();
        if(photonView!=null && photonView.isMine)
        {
            PlayerManagement.instance.ModifyHealth(photonView.owner, -10);
        }
    }
}
