using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public float health = 100;
    PlayerMovement movement;
    bool dead;

    float shootTimer;
    float timeBetweenShots = 1f;

    public GameObject projectilePrefab;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            movement.controllable = false;

            movement.deathProgress = 0;

            movement.cacheCamStartPos = movement.cameraPosTf.position;
            movement.cacheCamEndPos = movement.cameraPosTf.position + movement.cameraPosTf.forward * 1.5f;
            movement.cacheSpeed = movement.plane.transform.forward * movement.moveSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectilePrefab, movement.plane.transform.position, movement.plane.transform.rotation);
            projectile.GetComponent<Projectile>().owner = this;
        }
    }
}
