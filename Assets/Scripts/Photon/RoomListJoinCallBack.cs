using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomListJoinCallBack : MonoBehaviourPunCallbacks
{
    static public Action onConnectedToMaster;

    public override void OnConnectedToMaster()
    {
        Debug.Log($"OnConnectedToMaster: Next -> {onConnectedToMaster.Method}");
        onConnectedToMaster?.Invoke();
    }

    Dictionary<string, RoomInfo> roomInfos = new Dictionary<string, RoomInfo>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("현재 방 개수:" + roomList.Count);
        RoomListJoinUI.Instance.OnRoomListUpdate(roomList);
    }


    [PunRPC]
    void ReceiveMessage(string str)
    {
        Debug.Log($"네트워크에서 받은 메시지 : {str}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("<Color=Red>OnDisconnected</Color> " + cause);
        RoomListJoinUI.Instance.IsInRoom = false;
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");

        // 게임 UI보여주자.
        InGameUI.Instance.Show();

        // 아웃 게임 UI 숨기기
        RoomListJoinUI.Instance.IsInRoom = true;

        InGameUI.Instance.Show();

        photonView.RPC(nameof(ReceiveMessage), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.NickName} 방에 들어옴");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom:" + newPlayer.NickName);
        PlayerListUI.Instance.OnPlayerEnteredRoom(newPlayer);

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom:" + otherPlayer.NickName);
        PlayerListUI.Instance.OnPlayerLeftRoom(otherPlayer);
    }
}
