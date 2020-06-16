using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManagerSingleplayer : MonoBehaviour
{
    public static GameManagerSingleplayer instance;

    public GameObject[] planes;

    public List<GameObject> spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject player = Instantiate(planes[Random.Range(0, planes.Length)], spawnPoint.position, spawnPoint.rotation);
        player.GetComponent<Player>().mode = Gamemode.Multiplayer;
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene(0);
    }
}
