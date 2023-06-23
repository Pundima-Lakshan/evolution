using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScoreUISpawner : NetworkBehaviour
{
    [SerializeField] private GameObject Score;

    [Command]
    public void cmdSpawnUI()
    {
        GameObject ScoreUI = Instantiate(Score);
        NetworkServer.Spawn(ScoreUI, connectionToClient);

    }
    
    bool isSpawned = false;
    void Update()
    {
        Debug.Log("NetworkClient.ready: " + NetworkClient.ready);
        if (isOwned && !isSpawned && NetworkClient.ready)
        {   
            cmdSpawnUI();
            isSpawned = true;
        }
    }
}
