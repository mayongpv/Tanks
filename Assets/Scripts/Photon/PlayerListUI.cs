using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListUI : SingletonMonoBehavior<PlayerListUI>
{
    PlayerInfoListItem basePlayerInfoListItem;

    void Start()
    {
        basePlayerInfoListItem = transform.Find("Scroll View/Viewport/Content/PlayerItem").GetComponent<PlayerInfoListItem>();
        basePlayerInfoListItem.gameObject.SetActive(false);
    }

    public void OnPlayerEnteredRoom(Player roomList)
    {
        var room = Instantiate(basePlayerInfoListItem);
        room.transform.SetParent(basePlayerInfoListItem.transform.parent);
        room.gameObject.SetActive(true);
        room.Init(roomList);
    }

    internal void OnPlayerLeftRoom(Player otherPlayer)
    {
        var leftPlayer = PlayerInfoListItem.Items.Find(x => x._UserId == otherPlayer.UserId);
        Destroy(leftPlayer.gameObject);
        //PlayerInfoListItem.Items.Remove(leftPlayer); // Destroy 될때 remove되니깐 여기서 할 필요 없음.
    }
}
