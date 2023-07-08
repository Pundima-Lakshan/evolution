 using UnityEngine;

public class SpawnCreature : MonoBehaviour
{

    [SerializeField] private GameObject[] creaturePrefabs;
    [SerializeField] private GameObject creatureParentObject;

    public static SpawnCreature instance;

    private void Awake() {
        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
    }

    public void SpawnTheCreature(GameObject parentCreature) {
        System.Type parentCreatureObjectType = parentCreature.GetType();
        GameObject spawnedCreature = Instantiate(creaturePrefabs[0], creatureParentObject.transform.position, Quaternion.identity);

        // GameObject newObject = Instantiate(prefab, position, rotation);
        // GameObject newParent = Instantiate(parent, Vector3.zero, Quaternion.identity);

        spawnedCreature.transform.SetParent(creatureParentObject.transform);
        spawnedCreature.transform.position = parentCreature.transform.position;
        spawnedCreature.transform.rotation = parentCreature.transform.rotation;
        spawnedCreature.transform.localScale = parentCreature.transform.localScale;

        Creature creatureComponentOfSpawned = spawnedCreature.GetComponent<Creature>();
        if (creatureComponentOfSpawned == null) {
            Debug.Log("Couldnt get new creature component reproduction");
            return;
        }

        Creature creatureComponentOfParent = parentCreature.GetComponent<Creature>();
        if (creatureComponentOfParent == null) {
            Debug.Log("Couldnt get old creature component reproduction");
            return;
        }

        float _maxHealth = UnityEngine.Random.Range(creatureComponentOfParent.creatureData.maxHealth - creatureComponentOfParent.creatureData.maxHealth * creatureComponentOfParent.creatureData.mutationRatio, creatureComponentOfParent.creatureData.maxHealth + creatureComponentOfParent.creatureData.maxHealth * creatureComponentOfParent.creatureData.mutationRatio);
        float _maxAge = UnityEngine.Random.Range(creatureComponentOfParent.creatureData.maxAge - creatureComponentOfParent.creatureData.maxAge * creatureComponentOfParent.creatureData.mutationRatio, creatureComponentOfParent.creatureData.maxAge + creatureComponentOfParent.creatureData.maxAge * creatureComponentOfParent.creatureData.mutationRatio);
        float _moveSpeed = UnityEngine.Random.Range(creatureComponentOfParent.creatureData.moveSpeed - creatureComponentOfParent.creatureData.moveSpeed * creatureComponentOfParent.creatureData.mutationRatio, creatureComponentOfParent.creatureData.moveSpeed + creatureComponentOfParent.creatureData.moveSpeed * creatureComponentOfParent.creatureData.mutationRatio);
        float _sensorDistance = UnityEngine.Random.Range(creatureComponentOfParent.creatureData.sensorDistance - creatureComponentOfParent.creatureData.sensorDistance * creatureComponentOfParent.creatureData.mutationRatio, creatureComponentOfParent.creatureData.sensorDistance + creatureComponentOfParent.creatureData.sensorDistance * creatureComponentOfParent.creatureData.mutationRatio);

        creatureComponentOfSpawned.creatureData.SetCreatureData(_maxHealth, _maxAge, _moveSpeed, _sensorDistance);
    }

}