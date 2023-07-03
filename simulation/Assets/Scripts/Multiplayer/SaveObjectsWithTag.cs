using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveObjectsWithTag : MonoBehaviour
{
    public GameObject creatureParentObject;
    public GameObject gameManager;
    public string tagToFind;
    public string filename;

    public GameObject Prefab;

    // Go to multiplayer scene
    public void StartMultiplayer()
    {
        Debug.Log("Starting multiplayer");
        Transform child = creatureParentObject.transform.GetChild(0);
        Debug.Log(child.name);
        SaveCreatureDetails(child.GetComponent<Creature>());
        // distroy gamemanaager
        Destroy(gameManager);
        SceneManager.LoadScene("Lobby");
    }

    private void SaveCreatureDetails(Creature creature)
    {
        // Path to the file
        string filePath = Application.dataPath + "/Resources/player.txt";
        FileStream stream = new FileStream(filePath, FileMode.Create);

        CreatureData data = new CreatureData(creature);

        // Convert the 2D array to a string
        string dataString = "";
        dataString += data.age;

        // Write the data to the file
        using (StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(dataString);
        }
        stream.Close();
    }
}