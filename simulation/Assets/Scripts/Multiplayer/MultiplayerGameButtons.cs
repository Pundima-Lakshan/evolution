using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerGameButtons : MonoBehaviour
{
    public void exit()
    {
        // Remove donotdestroyonload objects
        Destroy(GameObject.Find("NetworkManager"));
        Destroy(GameObject.Find("CreaturesData"));
        Destroy(GameObject.Find("LevelManager"));
        Destroy(GameObject.Find("GamePlayer(Clone)"));
        Destroy(GameObject.Find("GamePlayer(Clone)"));


        // Load the menu scene
        SceneManager.LoadScene("MenuScene");
    }
}
