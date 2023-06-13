using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField] private Vector2 minPosition = new Vector2(-40, -20);
    [SerializeField] private Vector2 maxPosition = new Vector2(40, 20);

    [SerializeField] private GameObject foodSourcePrefab;

    [SerializeField] private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCreature();
        SpawnCreature();
        SpawnCreature();
    }

    void SpawnCreature() {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), 0);
        GameObject spawnedCreature = Instantiate(foodSourcePrefab, spawnPosition, Quaternion.identity);
        spawnedCreature.transform.SetParent(parentObject.transform);
    }
}
