using Mirror;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DapperDino.Mirror.Tutorials.Lobby
{
    public class MultiplayerCreatureSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject[] creaturePrefabs;

        [SerializeField] private GameObject parent;

        private float spawnRange = 10f;

        // Find CreaturesData object and return the age
        private float loadCreatureDetails()
        {
            GameObject creaturesData = GameObject.Find("CreaturesData");
            if (creaturesData != null) {
            } else {
                Debug.Log("CreaturesData not found");
            }

            return creaturesData.GetComponent<CreaturesData>().value;
        }


        [Command]
        public void CmdSpawnCreature(float _age, string name, uint netId)
        {
            // Check if the parent object is marked as DontDestroyOnLoad
            if (parent.scene.name == null)
            {
                Debug.Log("Donotdistroy parent found");
                // Instantiate a new instance of the parent object at 0,0,0
                GameObject newParent = Instantiate(parent, Vector3.zero, Quaternion.identity);


                // Set the parent variable to the new instance of the parent object
                parent = newParent;
            }

            // Changing parent name to player name
            if (parent.name != name)
                Debug.Log("Parent name changed to " + name);
                parent.name = name;
            
            GameObject creatureInstance;
            if (parent == null)
                Debug.Log("Null parent");
            else {
                NetworkServer.Spawn(parent, connectionToClient);
                creatureInstance = Instantiate(creaturePrefabs[netId % 4], parent.transform);
                creatureInstance.GetComponent<Creature>().age = _age;
                NetworkServer.Spawn(creatureInstance, connectionToClient);
            }
                
            // Debug.Log(creatureInstance.GetComponent<Creature>().age);
            
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));
            return randomSpawnPosition;
        }
        private bool isFinished = false;
        // Client code
        void Update()
        {
            // if not finished and in Multiplayer scene
            if(!isFinished && SceneManager.GetActiveScene().name == "MultiplayerScene" && NetworkClient.ready)
            {
            
                if (isOwned)
                {
                    // Debug.Log("isOwned MultiplayerCreatureSpawner");
                    for (int i = 0; i < 10; i++)
                    {
                        // Debug.Log("Spawn creature MultiplayerCreatureSpawner");
                        CmdSpawnCreature(loadCreatureDetails(), PlayerNameInput.DisplayName, this.GetComponent<NetworkIdentity>().netId);
                    }
                    
                    isFinished = true;
                }
                ScoreScript.instance.LoadPlayersAndScores();
            }
        }
    }
}
