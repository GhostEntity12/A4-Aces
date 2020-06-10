using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviourPun, IPunObservable
{
    [Tooltip("Script that deals with the player's movement")]
    PlayerMovement movement;
    public List<Behaviour> playerInputsAndBehaviours = new List<Behaviour>();

    [Header("Stats")]
    [Tooltip("How much health the player has")]
    public float currentHealth = 100;
    [Tooltip("The maximum health the player can have")]
    public float maxHealth = 100;
    [Tooltip("Exists to make sure the death event only triggers once")]
    bool dead;
    [Tooltip("Keeps track of the score in singleplayer")]
    int score;

    [Header("Projectiles")]
    [Tooltip("The minimum time between shots")]
    public float timeBetweenShots = 1f;
    [Tooltip("Timer for how long since the last shot")]
    float shootTimer;
    [Tooltip("How much ammo the player currently has")]
    int ammo = 10;

    [Tooltip("The prefab that prjectiles are spawned from")]
    public GameObject projectilePrefab;


    private void Awake()
    {
        if (photonView.IsMine)
        {
            foreach (Behaviour behaviour in playerInputsAndBehaviours)
            {
                behaviour.enabled = true;
            }
            // Setting variables
            movement = GetComponent<PlayerMovement>();

            // Error checking
            Projectile projectileScript = projectilePrefab.GetComponent<Projectile>();
            // Missing script
            if (projectileScript == null)
            {
                Debug.LogError("Projectile prefab needs a Projectile component!");
            }
            // Invalid speed
            else if (projectileScript.speed <= movement.moveSpeed)
            {
                Debug.LogError($"Projectile movement speed ({projectileScript.speed}) should be larger than the player's movement speed ({movement.moveSpeed})!");
            }
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        // On start death
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            movement.controllable = false;

            // For death timer
            movement.deathProgress = 0;

            // Caching positions for post death camera movement
            movement.cacheCamStartPos = movement.cameraPosTf.position;
            movement.cacheCamEndPos = movement.cameraPosTf.position + movement.cameraPosTf.forward * 1.5f;
            movement.cacheSpeed = movement.plane.transform.forward * movement.moveSpeed;
        }

        if (shootTimer >= timeBetweenShots && ammo > 0)
        {
            // On tap
            if (Input.GetMouseButton(0))
            {
                // Resets timer
                shootTimer = 0;

                // Spawn projectile
                GameObject projectile = PhotonNetwork.Instantiate(projectilePrefab.name, movement.plane.transform.position, movement.plane.transform.rotation);
                // Set owner
                projectile.GetComponent<Projectile>().owner = this;
                // Ignore collisions between the projectile and the owner
                Physics.IgnoreCollision(movement.plane.GetComponent<Collider>(), projectile.GetComponent<Collider>());
                // Randomise projectile color
                projectile.GetComponent<Renderer>().materials[0].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
        }
        else
        {
            // Increments timer
            shootTimer += Time.deltaTime;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        // TODO: Update Score UI
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Client player, send data
            stream.SendNext(currentHealth);
        }
        else
        {
            // Network player, receive data
            this.currentHealth = (float)stream.ReceiveNext();
        }
    }
}
