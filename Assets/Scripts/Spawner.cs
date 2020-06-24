using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviourPun
{
    public Gamemode gamemode;

    public System.Type type;

    [Range(0, 1), Tooltip("What proportion of objects should be active at any given time")]
    public float spawnProportion = 0.5f;

    public List<MonoBehaviour> allBehaviours = new List<MonoBehaviour>();
    public List<MonoBehaviour> inactiveBehaviours = new List<MonoBehaviour>();

    // Start is called before the first frame update
    void Awake()
    {
        type = allBehaviours[0].GetType();

        List<MonoBehaviour> badBehaviours = new List<MonoBehaviour>();
        foreach (MonoBehaviour behaviour in allBehaviours)
        {
            if (behaviour.GetType() != type)
            {
                Debug.LogError($"{behaviour.name} is of type {behaviour.GetType()} - should be of type {type}");
                badBehaviours.Add(behaviour);
                continue;
            }
            if (!behaviour.gameObject.activeSelf)
            {
                inactiveBehaviours.Add(behaviour);
            }
        }

        foreach (MonoBehaviour behaviour in badBehaviours)
        {
            allBehaviours.Remove(behaviour);
        }
    }

    void Update()
    {
        if (gamemode == Gamemode.Multiplayer)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            // Enables objects randomly until the threshhold is met
            while ((float)inactiveBehaviours.Count / allBehaviours.Count > spawnProportion)
            {
                // inactiveObjects.Count - 1 prevents the game from respawning the most recently despawned
                MonoBehaviour selection = inactiveBehaviours[Random.Range(0, inactiveBehaviours.Count - 1)];
                selection.GetComponent<PhotonView>().RPC("SetState", RpcTarget.AllBufferedViaServer, true);
                inactiveBehaviours.Remove(selection);
            }
        }
        else if (gamemode == Gamemode.Singleplayer)
        {
            while ((float)inactiveBehaviours.Count / allBehaviours.Count > spawnProportion)
            {
                MonoBehaviour selection = inactiveBehaviours[Random.Range(0, inactiveBehaviours.Count - 1)];
                selection.gameObject.SetActive(true);
                inactiveBehaviours.Remove(selection);
            }
        }
    }

    [PunRPC]
    public void SyncObjectState(int id, bool state)
    {
        if (state)
        {
            inactiveBehaviours.Remove(allBehaviours[id]);
        }
        else
        {
            inactiveBehaviours.Add(allBehaviours[id]);
        }
    }
}
