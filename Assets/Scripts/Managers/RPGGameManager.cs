using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RPGGameManager : MonoBehaviour
{

    [SerializeField] private RPGCameraManager cameraManager;

    public static RPGGameManager sharedInstance;

    [SerializeField] private SpawnPoint playerSpawnPoint;
    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Debug.Log("There are more than one instance of game manager exist");
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;  
        }

    }

    private void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();

            cameraManager.virtualCamera.Follow = player.transform;
            
        }
    }

}
