using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum MenuState
{
    Gamemode,
    SPLevels,
    Online
}
public class MenuManager : MonoBehaviourPunCallbacks
{
    string gameVersion;

    GameObject currentState;

    [Header("Singleplayer")]

    [Header("Online")]
    public TextMeshPro connectButton;
    public TextMeshPro connectingText;

    public TextMeshPro room1Text;
    public TextMeshPro room2Text;

    public string room;

    bool isConnecting;

    // Start is called before the first frame update
    void Awake()
    {
        gameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public void ReturnToMain()
    {

    }

    public void Connect()
    {
        connectingText.text = "Connecting to PUN's multiplayer services...";
        if (!PhotonNetwork.IsConnected)
        {
            connectButton.gameObject.SetActive(false);
            connectingText.gameObject.SetActive(true);
            print("Connecting to Photon Servers");
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            connectingText.gameObject.SetActive(false);
            room1Text.gameObject.SetActive(true);
            room2Text.gameObject.SetActive(true);
            isConnecting = false;
        }
    }

    public void JoinRoom(string _room)
    {
        room = _room;
        PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions { MaxPlayers = 20 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(room);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectingText.text = $"Disconnected from PUN servers: {cause}";
    }
}
