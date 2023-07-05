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

        private GameObject[] gamePlayers;
        private CreatureData loadCreatureDetails()
        {
            GameObject creaturesData = GameObject.Find("CreaturesData");
            if (creaturesData != null) {
            } else {
                Debug.Log("CreaturesData not found");
            }

            return creaturesData.GetComponent<CreaturesData>().creaturesData[0];
        }


        [Command]
        public void CmdSpawnCreature(CreatureData data, string name, uint netID)
        {
            // If gamePlayers is empty, find all players
            if (gamePlayers == null)
            {
                gamePlayers = GameObject.FindGameObjectsWithTag("Gameplayer");
            }
            Debug.Log("Size of GamePlayers: " + gamePlayers.Length);
            // Check if the parent object is marked as DontDestroyOnLoad
            if (parent.scene.name == null)
            {
                // Instantiate a new instance of the parent object at 0,0,0
                GameObject newParent = Instantiate(parent, Vector3.zero, Quaternion.identity);


                // Set the parent variable to the new instance of the parent object
                parent = newParent;
            }

            // Changing parent name to player name
            if (parent.name != name)
                parent.name = name;
            
            // // If netID is not in the list, add it to the list
            // if (!netIDs.Contains(netID))
            // {
            //     Debug.Log("Adding netID" + netID);
            //     // Add the netID to the next index in the list
            //     netIDs.Add(netID);
            // }
                
            GameObject creatureInstance;
            if (parent == null)
                Debug.Log("Null parent");
            else {
                NetworkServer.Spawn(parent, connectionToClient);
                creatureInstance = Instantiate(creaturePrefabs[(gamePlayers.Length * 2 - netID) % 4], parent.transform);
                creatureInstance.GetComponent<Creature>().creatureData = data;
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
                    
                    for (int i = 0; i < 10; i++)
                    {
                        CmdSpawnCreature(loadCreatureDetails(), PlayerNameInput.DisplayName, GetComponent<NetworkIdentity>().netId);
                    }
                    
                    isFinished = true;
                }
                if (ScoreScript.instance != null)
                {
                    ScoreScript.instance.LoadPlayersAndScores();
                }
                
            }
        }
    }
}
