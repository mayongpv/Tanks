using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviourPun
{  
    void Update()
    {
        if (photonView.IsMine == false)
            return;

         Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.DownArrow))
            move.z = 1;
        if (Input.GetKey(KeyCode.UpArrow))
            move.z = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            move.x = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            move.x = 1;

        if (move.sqrMagnitude > 0)
                {
            //방향
            move.Normalize();
            transform.Translate(move * speed * Time.deltaTime, Space.World);

            transform.forward = move;
        }

    }
    public float speed = 5;
}
