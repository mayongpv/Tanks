using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinOrCreateRoomUI : MonoBehaviourPunCallbacks
{
    SaveString userName;
    GameObject outGameUiGo;
    void Start()
    {
        userName = new SaveString("userName");

        outGameUiGo = transform.Find("UI").gameObject;
        outGameUiGo.transform.Find("EnterButton").GetComponent<Button>().AddListener(this, Connect);
        InputField inputField = outGameUiGo.transform.Find("UserName").GetComponent<InputField>();
        inputField.AddListener(this, (string str) => { userName.Value = str; });
        inputField.text = userName.Value;
    }

    [PunRPC]
    void ReceiveMessage(string str)
    {
        Debug.Log($"네트워크에서 받은 메시지 : {str}");
    }

    private void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = userName.Value;

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Joining Room...");
            JoinOrCreateRoom();
        }
        else
        {
            Debug.Log("Connecting...");
            PhotonNetwork.GameVersion = this.gameVersion; // 필수는 아님.
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    private static void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("AutoCreateRoom"
                    , new RoomOptions() { IsVisible = true }
                    , new TypedLobby("LogtypeString", LobbyType.Default));
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
    }


    bool isInRoom;

    [SerializeField]
    private string gameVersion = "1";

    bool IsInRoom
    {
        set
        {
            isInRoom = value;
            outGameUiGo.SetActive(!isInRoom); // 룸에 있으면 outGameUi는 안보여야한다.
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster: Next -> JoinOrCreateRoom");
        JoinOrCreateRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("<Color=Red>OnDisconnected</Color> " + cause);
        IsInRoom = false;
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");

        // 게임 UI보여주자.
        IsInRoom = true;
        photonView.RPC(nameof(ReceiveMessage), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.NickName} 방에 들어옴");

        //유저가 들어오면 조정 가능한 탱크를 소환하자
        PhotonNetwork.Instantitate(tankPrefabName, new vector3(0f, 0, 0), Quaterinion.identity, 0;
    }
    public sting tankPrefabName = "Tank"

        [PunRPC]
        void ReceiveMessage(string str)

    }
}
