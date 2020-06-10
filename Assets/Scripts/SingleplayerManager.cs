using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetupManager : MonoBehaviour
{
    public bool loadInSingleplayer;
    public List<string> singleplayerEnabledLevels;
    bool inSingleplayerMode;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (loadInSingleplayer && singleplayerEnabledLevels.Contains(scene.name))
        {
            Debug.Log($"Loading {scene} in singleplayer mode");
            inSingleplayerMode = true;
            // Run singleplayerSetup
        }
        else
        {
            inSingleplayerMode = false;
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!inSingleplayerMode) return;

    }

}
