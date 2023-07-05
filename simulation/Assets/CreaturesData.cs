using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreaturesData : MonoBehaviour
{
    public static CreaturesData instance;

    public GameObject gameManager;

    public GameObject creatureParentObject;
    public float value;

    // An array to store data of creatures
    public List<CreatureData> creaturesData = new List<CreatureData>();

    // Start is called before the first frame update
    public void GetData()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // Get data from creatures
        for (int i = 0; i < creatureParentObject.transform.childCount; i++) {
            Transform child = creatureParentObject.transform.GetChild(i);
            creaturesData.Add(child.GetComponent<Creature>().creatureData);
        }


        Transform exCreature = creatureParentObject.transform.GetChild(0);
        value = exCreature.GetComponent<Creature>().age;
    }

    public void StartMultiplayer()
    {
        Destroy(gameManager);
        SceneManager.LoadScene("Lobby");
    }

    
}
