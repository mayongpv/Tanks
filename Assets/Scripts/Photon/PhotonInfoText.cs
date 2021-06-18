using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonInfoText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    void Update()
    {
        text.text = $"IsConnected : {PhotonNetwork.IsConnected}" +
            $"\nIsConnected : {PhotonNetwork.IsConnected}" +
            $"\nIsMasterClient : {PhotonNetwork.IsMasterClient}" +
            $"\nIsConnectedAndReady : {PhotonNetwork.IsConnectedAndReady}" +
            $"\nInLobby : {PhotonNetwork.InLobby}" +
            $"\nInRoom : {PhotonNetwork.InRoom}" +
            $"\nCountOfRooms : {PhotonNetwork.CountOfRooms}" +
            $"\nCountOfPlayers : {PhotonNetwork.CountOfPlayers}" +
            $"\nCountOfPlayersInRooms : {PhotonNetwork.CountOfPlayersInRooms}";
    }
}
