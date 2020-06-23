using Photon.Pun;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviourPun
{
    public Gamemode gamemode;
    public Player owner;
    [Min(2), Tooltip("Speed of the projectile. Should be larger than the player's movement speed")]
    public float speed = 2;
    public float damage;
    public float lifetime = 40f;
    float life = 0f;
    public Rigidbody rb;
    bool stuck;
    Renderer r;

    private void Awake()
    {
        // Setting variables
        rb = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();
        if (gamemode == Gamemode.Singleplayer)
        {
            RandomiseColor();
        }
        else
        {
            photonView.RPC("RandomiseColor", RpcTarget.AllBufferedViaServer);
        }
    }

    private void Update()
    {
        // If the projectile is not yet stuck, move the projectile forwards
        if (!stuck)
        {
            rb.velocity = transform.forward * speed;
        }

        // Increments death counter and destroys object if it's existed for more than lifetime seconds
        life += Time.deltaTime;
        if (life >= lifetime)
        {
            if (gamemode == Gamemode.Multiplayer)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile is already stuck, ignore
        if (stuck) return;

        // Check whether the other object hit is a player
        Player hitPlayer = collision.transform.GetComponentInParent<Player>();
        Target t = collision.transform.GetComponent<Target>();

        // Not a player
        if (hitPlayer == null)
        {
            if (t != null)
            {
                life = lifetime;
            }
            // Freeze the projectile and exit
            stuck = true;
            rb.isKinematic = true;
            return;
        }
        // Is a player
        else
        {
            // subtract health from the other player
            hitPlayer.currentHealth -= damage;
            // Destroys the object next frame (see Update)
            life = lifetime;
        }
    }

    [PunRPC]
    void RandomiseColor()
    {
        // Randomise projectile color
        Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        r.materials[1].color = c;
        r.materials[1].SetColor("_EmissionColor", c);
    }
}
