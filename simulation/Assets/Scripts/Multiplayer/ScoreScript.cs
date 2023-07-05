using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror;

public class ScoreScript : NetworkBehaviour
{
    public static ScoreScript instance;
    // Start is called before the first frame update
    public GameObject[] players;

    [SerializeField] private GameObject[] playerScores;

    public class SyncListInt : SyncList<int> { }
    public class SyncListString : SyncList<string> { }
    public SyncListInt creatureCount = new SyncListInt();
    public SyncListString playerNames = new SyncListString();

    [SerializeField] private GameObject gameOverScreen;

    
    private bool isCounted = false;


    [SyncVar(hook = nameof(OnGameOverChanged))]
    bool isGameOver = false;
    

    [ServerCallback]
    public void GameOver()
    {
        isGameOver = true;
    }
    
    void OnGameOverChanged(bool oldValue, bool newValue)
    {
        if(gameOverScreen == null)
        {
            Debug.Log("Game over screen not found");
            return;
        }
        // Activate the canvas inside the game over screen
        gameOverScreen.transform.GetChild(0).gameObject.SetActive(true);

        // Stop the game logic on the client
        if (newValue)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Initialize creature count to 4 zeros
        if(isServer)
        {
            for (int i=0; i < 4; i++)
            {
                creatureCount.Add(0);
                playerNames.Add("Player" + (i+1).ToString());
            }
        }

        // Find gameover screen
        gameOverScreen = GameObject.Find("GameOverScreen");

        Transform scoresobjectTransform = transform.Find("Scores");
        if(scoresobjectTransform == null) {
            Debug.Log("Scores object not found");
            return;
        }
        Transform player1Transform = scoresobjectTransform.Find("Player1");
        if(player1Transform == null) {
            Debug.Log("Player1 object not found");
            return;
        }
        Transform player2Transform = scoresobjectTransform.Find("Player2");
        if(player2Transform == null) {
            Debug.Log("Player2 object not found");
            return;
        }
        Transform player3Transform = scoresobjectTransform.Find("Player3");
        if(player3Transform == null) {
            Debug.Log("Player3 object not found");
            return;
        }
        Transform player4Transform = scoresobjectTransform.Find("Player4");
        if(player4Transform == null) {
            Debug.Log("Player4 object not found");
            return;
        }
        playerScores = new GameObject[4];
        playerScores[0] = player1Transform.gameObject;
        playerScores[1] = player2Transform.gameObject;
        playerScores[2] = player3Transform.gameObject;
        playerScores[3] = player4Transform.gameObject;

        
    }

    // Update is called once per frame
    void Update()
    {
        countCreatures();
        
    }

    private void countCreatures()
    {
        if(players.Length == 0)
        {
            Debug.Log("No players found");
        }

        for (int i=0; i < players.Length && i < playerScores.Length; i++)
        {
            //  Check if the plaerScores object is deactive
            if(!playerScores[i].activeSelf)
            {
                playerScores[i].SetActive(true);
            }
            Transform transformCreatureCount = playerScores[i].transform.Find("NoOfCreatures");
            if(transform == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            TextMeshProUGUI text = transformCreatureCount.GetComponent<TextMeshProUGUI>();
            if(text == null)
            {
                Debug.Log("Text is null");
                continue;
            }
            if(isServer)
            {
                // If palyer object is not distroyed
                if(players[i] != null)
                {
                    // Get the number of creatures
                    creatureCount[i] = players[i].transform.childCount;
                }
                else{
                    creatureCount[i] = 0;
                }
                
            }

            text.text = creatureCount[i].ToString();

        }

        // If playerScores only have three element with zero value
        int zeroCount = 0;
        for (int i=0; i < playerScores.Length; i++)
        {
            if(creatureCount[i] == 0)
            {
                zeroCount++;
            }
        }

        // Find wheather the scores were counted
        if(zeroCount < 3 && !isCounted)
        {
            isCounted = true;
            
        }

        if(isCounted && zeroCount == 3)
        {
            GameOver();
        }
        
    }

    public void LoadPlayersAndScores() {
        players = GameObject.FindGameObjectsWithTag("Multiplayer");
        if(players.Length == 0)
        {
            Debug.Log("No players found");
        }

        for (int i=0; i < players.Length; i++)
        {
            // Check if i is outside the bounds of the array
            if(i >= playerScores.Length)
            {
                Debug.Log("Player score array out of bounds");
                continue;
            }
            Transform transformName = playerScores[i].transform.Find("Name");
            if(transformName == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            TextMeshProUGUI textName = transformName.GetComponent<TextMeshProUGUI>();
            if(textName == null)
            {
                Debug.Log("Text is null");
                continue;
            }

            if(isServer)
            {
                // Get the player name
                playerNames[i] = players[i].name;
            }
            textName.text = playerNames[i];
            
        }
    }
}
