using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//public enum Scene
//{
//    Office,
//    Room
//}

public class MenuManager : MonoBehaviour
{
    NetworkManager nm;
    public OnlineMatchMaker omm;

    public GameObject menu;
    public GameObject menuPlayers;
    public GameObject menuConnection;
    public GameObject menuLan;
    public GameObject menuWiFi;

    [HideInInspector]
    public static List<string> scenesInBuild;
    private void Awake()
    {
        nm = NetworkManager.singleton;

        scenesInBuild = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }
    }

    public void JoinLan()
    {
        nm.StartClient();
    }

    public void HostServer(string scene)
    {
        if (!scenesInBuild.Contains(scene))
        {
            Debug.LogError($"Attempted to load invalid scene \"{scene}\" when creating room");
            return;
        }
        nm.onlineScene = scene;
        nm.StartServer();
    }

    public void JoinOnline(string scene)
    {
        if (!scenesInBuild.Contains(scene))
        {
            Debug.LogError($"Attempted to load invalid scene \"{scene}\" when joining online");
            return;
        }
        omm.FindInternetMatch(scene);
    }
}
