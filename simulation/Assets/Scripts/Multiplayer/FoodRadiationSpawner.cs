using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class FoodRadiationSpawner : NetworkBehaviour
{

    [SerializeField] private GameObject food;
    [SerializeField] private GameObject radiationZone;
    private float foodIntervalTime = 1f;
    private float radiationIntervalTime = 5f;
    private float radiationTimeCounter = 0;
    private float FoodTimeCounter = 0;

    [Command]
    public void CmdSpawnFood()
    {
        GameObject foodInstance;
        foodInstance = Instantiate(food, randomPosition(), new Quaternion(0f, 0f, 0f, 0f));
        NetworkServer.Spawn(foodInstance);
    }

    [Command]
    public void CmdSpawnRadiation()
    {
        GameObject radInstance;
        radInstance = Instantiate(radiationZone, randomPosition(), new Quaternion(0f, 0f, 0f, 0f));
        NetworkServer.Spawn(radInstance);
    }

    private Vector3 randomPosition()
    {
        // Define the range for the random position
        float minX = -60f;
        float maxX = 60f;
        float minY = -40f;
        float maxY = 40f;
        
        // Get a random position within the defined range
        Vector3 randomPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0
        );

        return randomPosition;
    }

    void Update()
    {
        FoodTimeCounter += 1f * Time.deltaTime;

        radiationTimeCounter += 1f * Time.deltaTime;
        
        if (radiationTimeCounter >= radiationIntervalTime)
            radiationTimeCounter = 0f; // Reset the timer

        if (FoodTimeCounter >= foodIntervalTime)
        {
            FoodTimeCounter = 0f;
            foodIntervalTime *= 2;
        }


        if(SceneManager.GetActiveScene().name == "MultiplayerScene" && NetworkClient.ready && isOwned && isServer)
        {
            if (FoodTimeCounter <= 0f)
                CmdSpawnFood();

            if (radiationTimeCounter <= 0f)
                CmdSpawnRadiation();
        }
    }
}
