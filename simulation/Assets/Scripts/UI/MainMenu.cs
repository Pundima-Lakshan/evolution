using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public void PlayGameBtnClick() {
        LevelManager.instance.LoadScene("GameScene");
    }

    public void QuitGameBtnClick() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
