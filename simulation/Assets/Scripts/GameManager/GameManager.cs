using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool isPaused = false;

    public static void ToggleIsPaused() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public static bool GetIsPaused() {
        return isPaused;
    }

    public static void IncrementSpeed() {
        if (Time.timeScale < 10)
            Time.timeScale *= 5;
        else
            Time.timeScale = 1;
    }

    public static void QuitGameBtnClick() {
        Application.Quit();
    }
}
