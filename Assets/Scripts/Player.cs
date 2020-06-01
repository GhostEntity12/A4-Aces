using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public float health = 100;
    PlayerMovement movement;
    bool dead;

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
            movement.cacheCamStartPos = movement.cameraPosTf.position;
            print(movement.cacheCamStartPos);
            movement.cacheCamEndPos = movement.cameraPosTf.position + movement.cameraPosTf.forward * 2;
            movement.cacheSpeed = movement.plane.transform.forward * movement.moveSpeed;
            movement.deathProgress = 0;
        }
    }
}
