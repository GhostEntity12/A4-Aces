﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Net.Sockets;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] planes;

    public List<GameObject> spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject player = PhotonNetwork.Instantiate(planes[Random.Range(0, planes.Length)].name, spawnPoint.position, spawnPoint.rotation);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}
