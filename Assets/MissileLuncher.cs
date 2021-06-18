using Photon.Pun;
using UnityEngine;
using System.Collections;
using System;

public class MissileLuncher : MonoBehaviourPun
{
    public GameObject missile;
    public Transform startPosition;

    public LayerMask layer;
    public Transform cannonBarrel;
    void Update()
    {
        if (photonView.IsMine == false)
            return;

)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PhotonNetwork.Instantiate(missile.name, startPosition.position, transform.rotation, 0);

        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitDate, 1000, layer))
            {
                var desPos = hitData.point;
                var dir = desPos - transform.position;
                cannonBarrel.forward = dir;
            }
        }


    }
}

