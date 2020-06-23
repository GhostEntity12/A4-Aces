using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Linq;

public class GameManagerMultiplayer : MonoBehaviourPunCallbacks
{
    public static GameManagerMultiplayer instance;

    public GameObject plane;

    public List<GameObject> spawnPoints;

    public GameObject deathRoom;

    GameObject player;

    [SerializeField]
    GameObject drCamera;

    CanvasGroup cg;

    private void Awake()
    {
        instance = this;
        cg = drCamera.GetComponentInChildren<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        SpawnNewPlayer();
        
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayerDied()
    {
        PhotonNetwork.Destroy(player);
        StartCoroutine(Fade.FadeElement(cg, 1, 1, 0));
        drCamera.SetActive(true);
    }

    public void SpawnNewPlayer()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject newPlayer = PhotonNetwork.Instantiate(plane.name, spawnPoint.position, spawnPoint.rotation);

        drCamera.SetActive(false);

        newPlayer.GetComponent<Player>().mode = Gamemode.Multiplayer;

        player = newPlayer;
    }
}
