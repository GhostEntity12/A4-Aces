using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManagerSingleplayer : MonoBehaviour
{
    public static GameManagerSingleplayer instance;

    public GameObject plane;

    public List<GameObject> spawnPoints;

    public GameObject deathRoom;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject player = Instantiate(plane, spawnPoint.position, spawnPoint.rotation);
        player.GetComponent<Player>().mode = Gamemode.Singleplayer;
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene(0);
    }
}
