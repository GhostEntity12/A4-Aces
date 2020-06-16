using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoRefuel : MonoBehaviourPun
{
    public int ammoRefuelAmount;
    int id;
    Gamemode gamemode;

    private void Start()
    {
        id = Spawner.instance.allAmmoRefuels.IndexOf(this);
        gamemode = Spawner.instance.gamemode;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponentInParent<Player>();
        if (p == null) return;

        p.currentAmmo = Mathf.Min(p.currentAmmo + ammoRefuelAmount, p.maxAmmo);

        if (gamemode == Gamemode.Multiplayer)
        {
            photonView.RPC("SetState", RpcTarget.AllBufferedViaServer, false);
            Spawner.instance.photonView.RPC("SyncObjectState", RpcTarget.AllBufferedViaServer, new object[] { id, false });
            Debug.Log($"{p.gameObject.name} claimed {ammoRefuelAmount} ammo from {gameObject.name}");
        }
        else if (gamemode == Gamemode.Singleplayer)
        {
            gameObject.SetActive(false);
            Debug.Log($"{p.gameObject.name} claimed {ammoRefuelAmount} ammo from {gameObject.name}");
            Spawner.instance.inactiveAmmoRefuels.Add(this);
        }
    }


    [PunRPC]
    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
