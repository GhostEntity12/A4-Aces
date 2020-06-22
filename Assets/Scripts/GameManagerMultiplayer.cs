using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Linq;

public class GameManagerMultiplayer : MonoBehaviour
{
    public static GameManagerMultiplayer instance;

    public GameObject plane;

    public List<GameObject> spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();
        print(spawnPoints.Count);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject player = PhotonNetwork.Instantiate(plane.name, spawnPoint.position, spawnPoint.rotation);
        player.GetComponent<Player>().mode = Gamemode.Multiplayer;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}
