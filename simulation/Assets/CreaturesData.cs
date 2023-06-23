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

        Transform child = creatureParentObject.transform.GetChild(0);
        value = child.GetComponent<Creature>().age;
    }

    public void StartMultiplayer()
    {
        Destroy(gameManager);
        SceneManager.LoadScene("Lobby");
    }

    
}
