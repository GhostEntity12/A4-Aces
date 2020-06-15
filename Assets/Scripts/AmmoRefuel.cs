using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoRefuel : MonoBehaviourPun
{
    public int ammoRefuelAmount;
    int id;

    private void Start()
    {
        id = Spawner.instance.allAmmoRefuels.IndexOf(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        Player p = other.gameObject.GetComponent<Player>();
        if (p == null) return;

        p.ammo += ammoRefuelAmount;

        photonView.RPC("SetState", RpcTarget.AllBufferedViaServer, false);
        Spawner.instance.photonView.RPC("SyncObjectState", RpcTarget.AllBufferedViaServer, new object[] { id, false });
        Debug.Log($"{p.gameObject.name} claimed {ammoRefuelAmount} ammo from {gameObject.name}");

    }


    [PunRPC]
    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
