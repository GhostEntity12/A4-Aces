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

    GameObject player;

    [SerializeField]
    CanvasGroup fade;

    public GvrPointerPhysicsRaycaster deathRoomRaycast;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        SpawnNewPlayer();
    }

    public void LeaveRoom()
    {
        StartCoroutine(Fade.FadeElement(fade, 1, 0, 1, callback: new Fade.CallbackDelegateInt(SceneManager.LoadScene), callbackInt: 0));
    }

    public void PlayerDied()
    {
        deathRoomRaycast.eventMask = ~0;
        Destroy(player);
        StartCoroutine(Fade.FadeElement(fade, 0.6f, 1, 0));
    }

    public void SpawnDelegate()
    {
        StartCoroutine(Fade.FadeElement(fade, 1, 0, 1, callback: SpawnNewPlayer));
    }

    public void SpawnNewPlayer()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject newPlayer = Instantiate(plane, spawnPoint.position, spawnPoint.rotation);

        Player p = newPlayer.GetComponent<Player>();

        p.mode = Gamemode.Singleplayer;

        deathRoomRaycast.eventMask = 0;

        player = newPlayer;

        fade.alpha = 1;
    }
}
