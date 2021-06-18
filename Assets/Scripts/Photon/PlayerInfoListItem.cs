using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoListItem : ListItemMonobehaviour<PlayerInfoListItem>
{
    public string _UserId;

    internal void Init(Player item)
    {
        this._UserId = item.UserId;
        Text infoText = transform.Find("infoText").GetComponent<Text>();
        infoText.text = $@"IsMaster:{item.IsMasterClient} {item.NickName} ({item.ActorNumber})";
    }
}
