using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableSphere : Photon.PunBehaviour {

    private void OnMouseDown()
    {
        this.photonView.RequestOwnership();
    }

    //Method below are called when "this.photonView.RequestOwnership();" fired
    public override void OnOwnershipRequest(object[] viewAndPlayer)
    {
        PhotonView view = viewAndPlayer[0] as PhotonView;
        PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;

        base.photonView.TransferOwnership(requestingPlayer);
    }

    // Update is called once per frame
    void Update () {
        if (this.photonView.owner == PhotonNetwork.player)//I'm the owner of sphere?
        {
            float moveSpeed = 4f;
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
            transform.position += transform.right * (horizontal * moveSpeed * Time.deltaTime);
        }
    }

    
}
