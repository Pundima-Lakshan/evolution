using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawn : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 minPosition = new Vector2(-40, -20);
    [SerializeField] private Vector2 maxPosition = new Vector2(40, 20);
    [SerializeField] private int numberOfObjects = 50;

    private void Start() {
        SpawnCreatures();
    }

    void SpawnCreatures() {
        for (int i = 0; i < numberOfObjects; i++) {
            Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), 0);
            GameObject spawnedCreature = Instantiate(prefab, spawnPosition, Quaternion.identity);
            InitializePrefab(spawnedCreature);
        }
    }

    void SpawnCreature() {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), 0);
        GameObject spawnedCreature = Instantiate(prefab, spawnPosition, Quaternion.identity);
        InitializePrefab(spawnedCreature);
    }

    void InitializePrefab(GameObject spawnedCreature) {
        // Your custom initialization code here, using the spawnedCreature reference
    }
}
