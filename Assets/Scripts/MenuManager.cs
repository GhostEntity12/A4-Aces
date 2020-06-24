using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.ComponentModel.Design;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviourPunCallbacks
{
    string gameVersion;

    public string[] levelNames = new string[2] { "Office0", "Office1" };

    public string singleplayerSuffix = "SP";

    [Header("Menus")]

    public GameObject activeStage;

    public GameObject gamemode, singleplayerLevels, multiplayerLevels;

    [Space(20)]
    public TextMeshPro connectingText;

    public string room;

    bool isConnecting;

    [SerializeField]
    CanvasGroup cg;

    [SerializeField]
    GvrPointerPhysicsRaycaster gvrRaycaster;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = Mathf.Min(60, Screen.currentResolution.refreshRate);
        activeStage = gamemode;
        gameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        StartCoroutine(Fade.FadeElement(cg, 1, 1, 0));
    }

    public void ReturnToMain()
    {
        activeStage.SetActive(false);
        activeStage = gamemode;
        activeStage.SetActive(true);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public void GoToSubmenuMenu(GameObject menu)
    {
        activeStage.SetActive(false);
        activeStage = menu;
        activeStage.SetActive(true);
    }

    public void Connect()
    {
        connectingText.text = "Connecting to PUN's multiplayer services...";
        if (!PhotonNetwork.IsConnected)
        {
            gamemode.gameObject.SetActive(false);
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
            GoToSubmenuMenu(multiplayerLevels);
            isConnecting = false;
        }
    }

    public void JoinRoom(int level)
    {
        gvrRaycaster.eventMask = 0;
        room = levelNames[level];
        PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions { MaxPlayers = 20 }, null);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(Fade.FadeElement(cg, 0.8f, 0, 1, PhotonNetwork.LoadLevel, room));
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectingText.text = $"Disconnected from PUN servers: {cause}";
    }

    public void LoadSingleplayer(int level)
    {
        gvrRaycaster.eventMask = 0;
        StartCoroutine(Fade.FadeElement(cg, 0.8f, 0, 1, UnityEngine.SceneManagement.SceneManager.LoadScene, levelNames[level] + singleplayerSuffix));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
