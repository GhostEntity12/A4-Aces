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
    public NetworkManager nm;

    public GameObject menu;
    public GameObject menuPlayers;
    public GameObject menuConnection;
    public GameObject menuLan;
    public GameObject menuWiFi;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinLan()
    {
        nm.StartClient();
    }

    public void HostServer(/*Scene*/string scene)
    {
        if (!SceneManager.GetSceneByName(scene).IsValid())
            throw new System.Exception($"Attempted to load invalid scene \"{scene}\"");
        nm.onlineScene = scene;
        nm.StartServer();
    }

    public void test()
    {
        
    }
}
