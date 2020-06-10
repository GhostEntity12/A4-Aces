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
    SceneSetupManager ssm;

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

    /// <summary>
    /// Join a local network game
    /// </summary>
    public void JoinLan()
    {
        nm.StartClient();
    }

    /// <summary>
    /// Start a local network game
    /// </summary>
    /// <param name="scene"></param>
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

    /// <summary>
    /// Join the online lobby
    /// </summary>
    /// <param name="scene"></param>
    public void JoinOnline(string scene)
    {
        if (!scenesInBuild.Contains(scene))
        {
            Debug.LogError($"Attempted to load invalid scene \"{scene}\" when joining online");
            return;
        }
        omm.FindInternetMatch(scene);
    }

    /// <summary>
    /// Start a singleplayer game
    /// </summary>
    /// <param name="scene"></param>
    public void StartSingleplayer(string scene)
    {
        if (!scenesInBuild.Contains(scene))
        {
            Debug.LogError($"Attempted to load invalid scene \"{scene}\" when starting singleplayer");
            return;
        }
        ssm.loadInSingleplayer = true;
        SceneManager.LoadScene(name);
    }
}
