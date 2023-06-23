using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour, IPointerClickHandler {

    public float age = 0;

    [SerializeField] private bool isControllable = false;

    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private float viewDistance = 1.5f;
    //[SerializeField] private bool canEat = true;
    //[SerializeField] private float size = 1f;
    //[SerializeField] private float energy = 100f;
    //[SerializeField] private float energyGained = 10f;
    //[SerializeField] private float numberOfChildren = 10f;
    //[SerializeField] private float lifespan = 1f;

    //[SerializeField] private float mutationChance = 0.8f;
    //[SerializeField] private float mutationAmount = 0.1f;

    //[SerializeField] private bool isDead = false;

    private List<GameObject> edibleFoodList = new List<GameObject>();

    [SerializeField] private Rigidbody2D rigibody2D;

    [SerializeField] private GameObject circle;

    // Start is called before the first frame update
    void Start() {
        rigibody2D = GetComponent<Rigidbody2D>();

        circle.transform.localScale = new Vector3(viewDistance, viewDistance, 1);

    }

    int count = 0;

    // Update is called once per frame
    void Update() {
        if (count > 30) {
            Move();
            count = 0;
        } else {
            count++;
        }

        if(count > 25) {
            GameObject closestFood = FindClosestFoodSource();
            if (closestFood != null) {
                Debug.Log(this.name + " Closest food is " + closestFood.name);
            }
        }

        if (GameManager.instance != null)
            age = GameManager.instance.GetGameTime();
    }

    private void UpdateSensesOfCreature() {

    }

    private void UpdateCreatureParameters() {

    }

    private void Move() {
        if (isControllable) {
            Vector3 inputDir = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W)) inputDir.y = +1f;
            if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;
            if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
            if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

            Vector3 moveDir = transform.up * inputDir.y + transform.right * inputDir.x;

            float moveSpeed = 30f;
            //transform.position += moveDir * moveSpeed * Time.deltaTime;

            rigibody2D.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        } else {
            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(-10, 10);
            Vector2 moveDirection2D = new Vector2(randomX, randomY);

            rigibody2D.velocity = moveDirection2D * moveSpeed;

            //moveDirection2D.Normalize();
            //transform.position += new Vector3(moveDirection2D.x * moveSpeed * Time.deltaTime, moveDirection2D.y * moveSpeed * Time.deltaTime, 0);
        }
     }

    private GameObject FindClosestFoodSource() {
        GameObject closestFood = null;
        float creatureX;
        float creatureY;

        float minFoodDistance = -1;

        creatureX = this.transform.position.x;
        creatureY = this.transform.position.y;

        //TODO: dynamically change the size of the sphere cast until it finds food to increase performance

        //use a sphere cast to find all food in range (determined by viewDistance) of the agent and add them to a list of edible food.
        //this helps optimize the code by not having to check every food object in the scene.
        edibleFoodList.Clear();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, viewDistance);
        foreach (var hit in hitColliders) {
            if (hit.gameObject.CompareTag("Food")) {
                edibleFoodList.Add(hit.gameObject);
            }
        }

        //find closest food in range of agent
        for (int i = 0; i < edibleFoodList.Count; i++) {
            if (edibleFoodList[i] != null) {
                float foodX = edibleFoodList[i].transform.position.x;
                float foodZ = edibleFoodList[i].transform.position.z;

                float foodDistance = Mathf.Sqrt((Mathf.Pow(foodX - creatureX, 2) + Mathf.Pow(foodZ - creatureY, 2)));
                if (foodDistance < minFoodDistance || minFoodDistance < 0) {
                    minFoodDistance = foodDistance;
                    if (minFoodDistance < viewDistance) {
                        closestFood = edibleFoodList[i];
                    }
                }
            }
        }

        return (closestFood);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void SaveCreatureHandle() {
        GameSave.SaveCreature(this);
    }

    public void LoadCreatureHandle() {
        CreatureData data = GameSave.LoadCreature();

        age = data.age;
    }
}
