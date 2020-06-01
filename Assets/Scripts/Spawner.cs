using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    [Range(0, 1)]
    public float spawnProportion = 0.5f;

    public List<GameObject> inactiveObjects = new List<GameObject>();
    float inactiveCount;
    public List<GameObject> activeObjects = new List<GameObject>();
    float activeCount;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject pickup in activeObjects)
        {
            RpcDisablePickup(pickup);
        }
    }

    // Update is called once per frame
    void Update()
    {
        inactiveCount = inactiveObjects.Count;
        activeCount = activeObjects.Count;
        while (inactiveCount / (inactiveCount + activeCount) < spawnProportion) 
        {
            RpcEnablePickup(inactiveObjects[Random.Range(0, inactiveObjects.Count)]);
        }
    }

    [ClientRpc]
    public void RpcEnablePickup(GameObject pickup)
    {
        pickup.SetActive(true);
        inactiveObjects.Remove(pickup);
        activeObjects.Add(pickup);
    }

    [ClientRpc]
    public void RpcDisablePickup(GameObject pickup)
    {
        pickup.SetActive(false);
        activeObjects.Remove(pickup);
        inactiveObjects.Add(pickup);
    }
}
