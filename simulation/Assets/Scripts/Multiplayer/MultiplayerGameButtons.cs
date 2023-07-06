using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerGameButtons : MonoBehaviour
{
    public void exit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
