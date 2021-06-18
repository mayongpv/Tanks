using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListJoinUI : BaseUI<RoomListJoinUI>
{
    SaveString userName;
    GameObject outGameUiGo;
    RoomInfoListItem baseRoomInfoListItem;

    Text selectedRoomText;

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var item in roomList)
        {
            if (item.RemovedFromList)
            {
                var removedRoom = RoomInfoListItem.Items.Find(x => x.roomName == item.Name);
                Destroy(removedRoom.gameObject);
            }
            else
            {
                var room = Instantiate(baseRoomInfoListItem);
                room.transform.SetParent(baseRoomInfoListItem.transform.parent);
                room.gameObject.SetActive(true);
                room.Init(item);
            }
        }
    }


    protected override void OnInit()
    {
        base.OnInit();

        InitUI();

        EnterLobbyOrConnect();
    }

    public override void Show()
    {
        base.Show();

        outGameUiGo.SetActive(true);
    }

    private void InitUI()
    {
        userName = new SaveString("userName");

        outGameUiGo = transform.Find("UI").gameObject;
        outGameUiGo.transform.Find("EnterButton").GetComponent<Button>().AddListener(this, JoinSelectedRoom);
        InputField inputField = outGameUiGo.transform.Find("UserName").GetComponent<InputField>();
        inputField.AddListener(this, (string str) => { userName.Value = str; });
        inputField.text = userName.Value;

        baseRoomInfoListItem = outGameUiGo.transform.Find("RoomList/Scroll View/Viewport/Content/RoomItem").GetComponent<RoomInfoListItem>();
        baseRoomInfoListItem.gameObject.SetActive(false);
        
        selectedRoomText = outGameUiGo.transform.Find("SelectedRoom/SelectedRoomText").GetComponent<Text>();

        outGameUiGo.transform.Find("CreateRoomButton").GetComponent<Button>().AddListener(this
            , CreateOrConnectRoom);
    }


    internal void SelectRoom(RoomInfoListItem roomInfoListItem)
    {
        selectedRoomText.text = roomInfoListItem.roomName;
    }


    private void JoinSelectedRoom()
    {
        if (string.IsNullOrEmpty(selectedRoomText.text))
        {
            Debug.Log("선택된 방이 없습니다");
            return;
        }

        Debug.Log(selectedRoomText.text + "방에 입장 요청 보냄");

        PhotonNetwork.LocalPlayer.NickName = userName.Value;
        PhotonNetwork.JoinRoom(selectedRoomText.text);
    }

    private void EnterLobbyOrConnect()
    {
        RunOrRunAfterConnect(JoinLobby);
    }

    void CreateOrConnectRoom()
    {
        RunOrRunAfterConnect(CreateRoom);
    }

    void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    private void RunOrRunAfterConnect(Action action)
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log($"{action.Method}");
            action();
        }
        else
        {
            Debug.Log("Connecting...");
            RoomListJoinCallBack.onConnectedToMaster = action;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = userName.Value;
        PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.NickName + "의 방",
            new RoomOptions() { MaxPlayers = 0 }); // 0은 인원 제한 안한다는 뜻.
    }

    bool isInRoom;

    public bool IsInRoom
    {
        set
        {
            isInRoom = value;
            outGameUiGo.SetActive(!isInRoom); // 룸에 있으면 outGameUi는 안보여야한다.
        }
    }
}
