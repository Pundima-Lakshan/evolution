using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror;

public class MultiplayerGameManager : NetworkBehaviour
{
    public static MultiplayerGameManager instance;
    // Start is called before the first frame update
    public GameObject[] players;


    [SerializeField] private GameObject[] playerScores;


    



    // [SyncVar(hook = nameof(OnMyVarChanged))]
    // public float myVar;

    // public void OnMyVarChanged(float oldVal, float newVal)
    // {
    //     //DISPLAY myvar on a text field
    //     Debug.Log("MyVar changed from " + oldVal + " to " + newVal);
    // }
    
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

        Transform canvasobjectTransform = transform.Find("Canvas");
        if(canvasobjectTransform == null) {
            Debug.Log("Canvas object not found");
            return;
        }
        Transform scoresobjectTransform = canvasobjectTransform.Find("Scores");
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
        if (isServer)
        {
            countCreatures();
            // myVar += Time.deltaTime * 0.1f;
        }
            

        
        
    }

    private void countCreatures()
    {
        if (isServer)
        {
            // Find players with tag "Multiplayer" and add them to players synclist
            players = GameObject.FindGameObjectsWithTag("Multiplayer");
        }

        

        

        for (int i=0; i < players.Length; i++)
        {
            // Debug.Log("Player names "+players[i].name);
            Transform transformNoOfCreatures = playerScores[i].transform.Find("NoOfCreatures");
            if(transformNoOfCreatures == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            TextMeshProUGUI textCreature = transformNoOfCreatures.GetComponent<TextMeshProUGUI>();
            if(textCreature == null)
            {
                Debug.Log("Text is null");
                continue;
            }

            Transform transformCreatureSprite = playerScores[i].transform.Find("CreatureSprite");
            if(transformCreatureSprite == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            SpriteRenderer spriteRendererCreature = transformCreatureSprite.GetComponent<SpriteRenderer>();
            if(spriteRendererCreature == null)
            {
                Debug.Log("Sprite renderer is null");
                continue;
            }
            spriteRendererCreature.sprite = players[i].transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void LoadPlayersAndScores() {
        if (isServer)
        {
            // Find players with tag "Multiplayer" and add them to players synclist
            players = GameObject.FindGameObjectsWithTag("Multiplayer");

        }

        for (int i=0; i < players.Length; i++)
        {
            // Debug.Log("Player names "+players[i].name);
            Transform transformPlayer = playerScores[i].transform.Find("Name");
            if(transformPlayer == null)
            {
                Debug.Log("Transform is null");
                continue;
            }
            
            TextMeshProUGUI textPlayer = transformPlayer.GetComponent<TextMeshProUGUI>();
            if(textPlayer == null)
            {
                Debug.Log("Text is null");
                continue;
            }
            
            textPlayer.text = players[i].name;
            
        }
    }
}
