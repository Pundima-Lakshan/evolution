using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Debug.Log("countCreatures");
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
            // Debug.Log("Player names "+players[i].name);
            Transform transform = playerScores[i].transform.Find("NoOfCreatures");
            if(transform == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            TextMeshProUGUI text = transform.GetComponent<TextMeshProUGUI>();
            if(text == null)
            {
                Debug.Log("Text is null");
                continue;
            }
            if(isServer)
            {
                Debug.Log("Player "+i+" has "+players[i].transform.childCount+" creatures");
                creatureCount[i] = players[i].transform.childCount;
            }

            Debug.Log("Player "+i+" has "+creatureCount[i]+" creatures");
            text.text = creatureCount[i].ToString();

            // transform = playerScores[i].transform.Find("CreatureSprite");
            // if(transform == null)
            // {
            //     Debug.Log("Transform is null");
            //     continue;
            // }
            // SpriteRenderer spriteRenderer = playerScores[i].transform.GetComponent<SpriteRenderer>();
            // if(spriteRenderer == null)
            // {
            //     Debug.Log("Sprite renderer is null");
            //     continue;
            // }
            // spriteRenderer.sprite = players[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
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
            // Debug.Log("Player names "+players[i].name);
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
                Debug.Log("Player "+i+" has "+players[i].transform.childCount+" creatures");
                // Get the player name
                playerNames[i] = players[i].name;
            }
            textName.text = playerNames[i];
            
        }
    }
}
