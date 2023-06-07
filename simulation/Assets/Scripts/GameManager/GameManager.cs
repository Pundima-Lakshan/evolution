using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool isPaused = false;

    public static void toggleIsPaused() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public static bool getIsPaused() {
        return isPaused;
    }

    public static void incrementSpeed() {
        if (Time.timeScale < 5)
            Time.timeScale *= 2;
        else
            Time.timeScale = 1;
    }
}
