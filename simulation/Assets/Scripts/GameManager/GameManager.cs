using UnityEngine;

using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float gameTime = 0f;

    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private GameObject creatureParentObject;
    [SerializeField] private GameObject lostScreen;

    private static bool isPaused = false;
    private static bool isLost = false;

    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject levelManager;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        gameTime = 0f;
        isPaused = false;
        isLost = false;
    }

    public float GetGameTime() {
        return gameTime;
    }

    private void Update() {
        if (isLost && !lostScreen.activeSelf) {
            lostScreen.SetActive(true);
            return;
        }

        gameTime += 0.5f * Time.deltaTime;
        gameTimeText.text = gameTime.ToString("0.00");

        if(creatureParentObject.transform.childCount <= 0) {
            //isLost = true;
        }        
    }

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
            Time.timeScale *= 4;
        else
            Time.timeScale = 1;
    }

    public static void QuitGameBtnClick() {
        Application.Quit();
    }

    public static void ChangeScene(string sceneName) {
        LevelManager.instance.LoadScene("MenuScene");
    }

    public void RestartGame() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Destroy(levelManager);
        Destroy(audioManager);
        Destroy(gameObject);
        
        SceneManager.LoadScene(currentSceneIndex);
    }
}
