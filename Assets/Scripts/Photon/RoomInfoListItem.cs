using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoListItem : ListItemMonobehaviour<RoomInfoListItem>
{
    public string roomName;

    new private void Awake()
    {
        base.Awake();
        gameObject.AddOrGetComponent<Button>().AddListener(this, OnClick);
    }

    void OnClick()
    {
        print(name + "클릭됨 <- 선택하자.");

        RoomListJoinUI.Instance.SelectRoom(this);
    }

    internal void Init(RoomInfo item)
    {
        roomName = item.Name;
        Text infoText = transform.Find("infoText").GetComponent<Text>();
        infoText.text =
$@"{item.Name}
                   <size=18> 참가 {item.PlayerCount} / 최대 {item.MaxPlayers}</size>";


    }

}
