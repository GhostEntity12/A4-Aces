using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Network behaviour for spawning pickups
/// </summary>
public class Spawner : NetworkBehaviour
{
    [Range(0, 1), Tooltip("What proportion of objects should be active at any given time")]
    public float spawnProportion = 0.5f;

    public List<GameObject> inactiveObjects = new List<GameObject>();
    public List<GameObject> activeObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Disables all active objects, just in case
        foreach (GameObject pickup in activeObjects)
        {
            RpcDisablePickup(pickup);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the counts of active and inactive objects
        float inactiveCount = inactiveObjects.Count;
        float activeCount = activeObjects.Count;

        // Enables objects randomly until the threshhold is met
        while (inactiveCount / (inactiveCount + activeCount) < spawnProportion)
        {
            RpcEnablePickup(inactiveObjects[Random.Range(0, inactiveObjects.Count)]);
        }
    }

    /// <summary>
    /// Enables a gameobject and moves it to the active list
    /// </summary>
    /// <param name="pickup">The object to enable</param>
    [ClientRpc]
    public void RpcEnablePickup(GameObject pickup)
    {
        pickup.SetActive(true);
        inactiveObjects.Remove(pickup);
        activeObjects.Add(pickup);
    }

    /// <summary>
    /// Disables a gameobject and moves it to the inactive list
    /// </summary>
    /// <param name="pickup">The object to disable</param>
    [ClientRpc]
    public void RpcDisablePickup(GameObject pickup)
    {
        pickup.SetActive(false);
        activeObjects.Remove(pickup);
        inactiveObjects.Add(pickup);
    }
}
