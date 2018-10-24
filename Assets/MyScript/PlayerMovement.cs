using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour {

    private PhotonView photonView;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    public float health;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update () {
        if (photonView.isMine)//Person try to control its object
            CheckInput();
        else
            SmoofMove();
	}

    private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            ///stream.SendNext(health); if I want send the current health to other player
        }
        else
        {
            targetPosition = (Vector3)stream.ReceiveNext();
            targetRotation = (Quaternion)stream.ReceiveNext();
            //health = (int)stream.ReceiveNext();
        }
    }

    private void SmoofMove()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);
    }

    private void CheckInput()
    {

        float moveSpeed = 100f;
        float rotateSpeed = 500f;
        float vertical = Input.GetAxis("Vertical");
        float horizontal= Input.GetAxis("Horizontal");

        transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontal * rotateSpeed * Time.deltaTime, 0));
    }
}
