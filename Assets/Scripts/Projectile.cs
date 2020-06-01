using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Player owner;
    [Min(2)]
    public float speed;
    public float damage;
    public Rigidbody rb;
    Transform projectileTf;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player hitPlayer = collision.transform.GetComponentInParent<Player>();
        if (hitPlayer == owner) return;

        if (hitPlayer != owner)
        {
            hitPlayer.health -= damage;
        }
        Destroy(gameObject);
    }
}
