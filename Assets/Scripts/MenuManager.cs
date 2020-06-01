using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    public void HostLan()
    {
        nm.StartHost();
    }

    public void JoinLan()
    {
        nm.StartClient();
    }

    public void HostServer()
    {
        nm.StartServer();
    }

    public void test()
    {
        
    }
}
