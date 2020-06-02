using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Player owner;
    [Min(2)]
    public float speed = 2;
    public float damage;
    public Rigidbody rb;
    Transform projectileTf;
    bool stuck;
    public float lifetime = 40f;
    float life = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!stuck)
        {
            rb.velocity = transform.forward * speed;
        }

        life += Time.deltaTime;
        if (life > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stuck) return;

        Player hitPlayer = collision.transform.GetComponentInParent<Player>();

        if (hitPlayer == null)
        {
            stuck = true;
            rb.isKinematic = true;
            return;
        }
        else
        {
            hitPlayer.health -= damage;
            life = lifetime;
        }
    }
}
