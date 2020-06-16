using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviourPun
{
    public Gamemode gamemode;

    public static Spawner instance;

    [Range(0, 1), Tooltip("What proportion of objects should be active at any given time")]
    public float spawnProportion = 0.5f;

    public List<AmmoRefuel> allAmmoRefuels = new List<AmmoRefuel>();
    public List<AmmoRefuel> inactiveAmmoRefuels = new List<AmmoRefuel>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        foreach (AmmoRefuel ammoRefuel in allAmmoRefuels)
        {
            if (!ammoRefuel.gameObject.activeSelf)
            {
                inactiveAmmoRefuels.Add(ammoRefuel);
            }
        }
    }

    void Update()
    {
        if (gamemode == Gamemode.Multiplayer)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            // Enables objects randomly until the threshhold is met
            while ((float)inactiveAmmoRefuels.Count / allAmmoRefuels.Count > spawnProportion)
            {
                // inactiveObjects.Count - 1 prevents the game from respawning the most recently despawned
                AmmoRefuel selection = inactiveAmmoRefuels[Random.Range(0, inactiveAmmoRefuels.Count - 1)];
                selection.photonView.RPC("SetState", RpcTarget.AllBufferedViaServer, true);
                inactiveAmmoRefuels.Remove(selection);
            }
        }
        else if (gamemode == Gamemode.Singleplayer)
        {
            while ((float)inactiveAmmoRefuels.Count / allAmmoRefuels.Count > spawnProportion)
            {
                AmmoRefuel selection = inactiveAmmoRefuels[Random.Range(0, inactiveAmmoRefuels.Count - 1)];
                selection.gameObject.SetActive(true);
                inactiveAmmoRefuels.Remove(selection);
            }
        }
    }

    [PunRPC]
    public void SyncObjectState(int id, bool state)
    {
        if (state)
        {
            inactiveAmmoRefuels.Remove(allAmmoRefuels[id]);
        }
        else
        {
            inactiveAmmoRefuels.Add(allAmmoRefuels[id]);
        }
    }
}
