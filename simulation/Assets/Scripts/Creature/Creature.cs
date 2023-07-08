using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class Creature : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private Rigidbody2D parentRigibody2D;
    public CreatureData creatureData;

    [SerializeField] private float age = 0f;
    [SerializeField] private float health = 20f;
    [SerializeField] private float hunger = 0f;
    [SerializeField] private float fitness = 0f;

    private float hungerGainRatio = 0.1f;
    private float hungerFoodReduceRatio = 1f;    
    
    private float healthRadiationDamageRatio = 2f;
    private float healthFoodGainRatio = 0.3f;

    private int maxChildren = 4;

    public bool isEating = false;
    private bool isDead = false;

    public List<GameObject> foodSourcesSensed = new List<GameObject>();
    public List<GameObject> radiationAreasSensed = new List<GameObject>();

    public Vector2 movingDirection;

    private bool isIndicatorShowing = false;

    private void CheckCircularSensors() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, creatureData.sensorDistance);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.CompareTag("Food")) {
                foodSourcesSensed.Add(collider.gameObject);
            } else if (collider.gameObject.CompareTag("Radiation")) {
                radiationAreasSensed.Add(collider.gameObject);
            }
        }
    }

    private void CheckSensorsNeuralNetwork() {

    }

    public int moveDirectionSetCount = 0;
    private int randomDirectionCount = 20;
    private void DetermineMovingDirection() {
        Vector2 moveDirection2D = movingDirection;

        float minFoodDistance = float.MaxValue;
        int foodSourceIndex = 0;

        if(!(foodSourcesSensed.Count > 0) || hunger < creatureData.hungerLimitToDeath) {
            if(moveDirectionSetCount > randomDirectionCount) {
                movingDirection = new Vector2(UnityEngine.Random.Range(-100, +100+1), UnityEngine.Random.Range(-100, +100+1)).normalized;
                moveDirectionSetCount= 0;
                return;
            }
            movingDirection = moveDirection2D;
            moveDirectionSetCount++;
            return;
        }

        Debug.Log("direction checked");

        // get closest food source
        for(int i = 0; i < foodSourcesSensed.Count; i++) {
            Vector2 foodSourcePosition = foodSourcesSensed[i].transform.position;
            float tempDistance = Vector2.Distance(foodSourcePosition, transform.position);
            if(tempDistance < minFoodDistance) {
                minFoodDistance = tempDistance;
                foodSourceIndex = i;
            }            
        }

        //float minRadiationDistance = float.MaxValue;
        //int radiationSourceIndex = 0;

        // avoid radiation zones
        //for(int i = 0; i < radiationAreasSensed.Count; i++) {
        //    Vector2 tempVector = transform.position - radiationAreasSensed[i].transform.position;
        //    float tempDistance = Vector2.Distance(tempVector, transform.position);
        //    if (tempDistance < minRadiationDistance) {
        //        minRadiationDistance = tempDistance;
        //        radiationSourceIndex = i;
        //    }
        //}

        //if(minRadiationDistance < minFoodDistance) {
            movingDirection = (foodSourcesSensed[foodSourceIndex].transform.position - transform.position).normalized;
        //} else {
        //    moveDirection2D = radiationAreasSensed[radiationSourceIndex].transform.position;
        //}     

        foodSourcesSensed.Clear();
        radiationAreasSensed.Clear();
    }

    private void DeterminMovingDirectionUsingNeuralNetwork() {

    }

    private void Eat() {
        if (hunger >= creatureData.hungerLimitToDeath) {
            hunger -= hungerFoodReduceRatio * Time.deltaTime;
        } else if(hunger >= 0) {
            isEating = false;
            hunger = 0;
        } else {
            isEating = false;
            hunger = 0;
        }

        if (health < creatureData.maxHealth && isEating) {
            health += healthFoodGainRatio * Time.deltaTime;
        } else {
            health = creatureData.maxHealth;
        }
    }

    private void GetRadiationDamaged() {
        if (health > 0) {
            health -= healthRadiationDamageRatio * Time.deltaTime;
        } else {
            health = 0;
            isDead = true;
        }
    }
    
    private void Reproduce() {
        SpawnCreature spawnCreature = new SpawnCreature();
        // code to spawn new child
        for(int i = 0; i < UnityEngine.Random.Range(1, maxChildren+1); i++) {
            SpawnCreature.instance.SpawnTheCreature(transform.gameObject);
        }
    }

    //private void MoveCreature() {
    //    if (isEating) {
    //        parentRigibody2D.velocity = Vector2.zero;
    //        return;
    //    }

    //    parentRigibody2D.velocity = movingDirection * creatureData.moveSpeed;
    //}

    private static float UpdateFitness(float age, float fitnessGene, float s) {
        double coefficient = 1 / (s * Math.Sqrt(2 * Math.PI));
        double exponent = -Math.Pow(age - fitnessGene, 2) / (2 * Math.Pow(s, 2));
        return Convert.ToSingle(coefficient * Math.Exp(exponent));
    }

    public void OnPointerClick(PointerEventData eventData) {
        isIndicatorShowing = !isIndicatorShowing;
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        Transform indicatorTransform = eventData.pointerCurrentRaycast.gameObject.transform.Find("Indicator");
        if (indicatorTransform == null) {
            Debug.Log("Indicator not found");
            return;
        }
        indicatorTransform.gameObject.SetActive(isIndicatorShowing);
        Transform indicatorValuesTransform = indicatorTransform.Find("IndicatorValues");
        if (indicatorValuesTransform == null) {
            Debug.Log("IndicatorValues not found");
            return;
        }
        TextMeshPro text = indicatorValuesTransform.GetComponent<TextMeshPro>();
        if (text == null) {
            Debug.Log("IndicatorValues TMP not found");
            return;
        }
        text.text = hunger.ToString() + "\n" + health.ToString() + "\n" + age.ToString();

        Reproduce();
    }

    private void UpdateCreatureIndicatorData(Transform creatureTransform) {
        if (!isIndicatorShowing)
            return;

        Transform indicatorTransform = creatureTransform.Find("Indicator");
        if (indicatorTransform == null) {
            Debug.Log("Indicator not found");
            return;
        }
        indicatorTransform.gameObject.SetActive(isIndicatorShowing);
        Transform indicatorValuesTransform = indicatorTransform.Find("IndicatorValues");
        if (indicatorValuesTransform == null) {
            Debug.Log("IndicatorValues not found");
            return;
        }
        TextMeshPro text = indicatorValuesTransform.GetComponent<TextMeshPro>();
        if (text == null) {
            Debug.Log("IndicatorValues TMP not found");
            return;
        }
        text.text = hunger.ToString() + "\n" + health.ToString() + "\n" + age.ToString();
    }

    private void Start() {
        Physics2D.alwaysShowColliders = true;

        health = creatureData.maxHealth;
        creatureData.reproductionHealth = creatureData.maxHealth;

        movingDirection = Vector2.one;

        string filePath = Application.dataPath + "saveData/saveDataWeights.json";
        Debug.Log(filePath);
    }

    bool isReproduced = false;

    private void Update() {
        // Death Update
        if (isDead) {
            Destroy(gameObject);
        }

        // Hunger Update
        hunger += hungerGainRatio * Time.deltaTime; // adjust to time it
        if (hunger > creatureData.hungerLimitToDeath) {
            isDead = true;
        }

        // Age Update measured by seconds
        age += 1f * Time.deltaTime; // adjust to time it

        // Fitness Update
        fitness = UpdateFitness(age, creatureData.fitnessGene, 1f);

        // Reproduce Update
        //if (fitness > creatureData.reproductionFitness && health >= creatureData.reproductionHealth && age >= creatureData.maxAge * creatureData.fitnessGene) {
        //    Reproduce();
        //}

        if (age >= creatureData.maxAge * 0.9f && !isReproduced)
        {
            Reproduce();
            isReproduced = true;
        }

        if(age >= creatureData.maxAge * 5)
        {
            isDead = true;
        }

        // Sensor Update
        //CheckCircularSensors();

        // Determine direction
        //DetermineMovingDirection();

        // Move Update
        //MoveCreature();

        // Update UI
        UpdateCreatureIndicatorData(transform);

    }

    // Other updates on collisions
    public void Collided(string name) {
        if (name == "Food" && hunger > creatureData.hungerLimitToDeath) {
            isEating = true;
            Debug.Log("isEating");
            Eat();
        } else if (name == "Radiation") {
            GetRadiationDamaged();
        }
    }
}