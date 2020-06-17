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

    [Header("Tiling")]
    [SerializeField]
    Vector2Int dimensions;

    float xJump, yJump;

    Renderer r;

    private void Awake()
    {
        instance = this;
        xJump = 1f / dimensions.x;
        yJump = 1f / dimensions.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject player = Instantiate(plane, spawnPoint.position, spawnPoint.rotation);
        int position = Random.Range(0, dimensions.x * dimensions.y);
        int xPos = Mathf.FloorToInt(position / dimensions.x);
        int yPos = position % dimensions.y;
        player.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(xPos * xJump, yPos * yJump));
        player.GetComponent<Player>().mode = Gamemode.Multiplayer;
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene(0);
    }
}
