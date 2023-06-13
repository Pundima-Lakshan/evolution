using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCreatureSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject creaturePrefab1;
    [SerializeField] private GameObject creaturePrefab2;
    [SerializeField] private GameObject creaturePrefab3;
    [SerializeField] private GameObject creaturePrefab4;
    [SerializeField] private GameObject creaturePrefab5;
    [SerializeField] private GameObject creaturePrefab6;

    private float spawnRange = 10f;

    [Command]
    public void CmdSpawnCreature()
    {
       
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject creatureInstance = Instantiate(creaturePrefab1, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(creatureInstance);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));
        return randomSpawnPosition;
    }

    // Client code
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOwned)
        {
            CmdSpawnCreature();
        }
    }
}
