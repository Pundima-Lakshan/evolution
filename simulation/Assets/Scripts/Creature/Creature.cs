using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class Creature : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private Rigidbody2D parentRigibody2D;

    public CreatureData creatureData;

    private const float fitnessGene = 0.5f; // 0 to 1 is it

    [SerializeField] private float age = 0f;
    private float fitness = 0f;

    [SerializeField] private float hunger = 0f;
    private float hungerFoodReduceRatio = 1f;
    private float hungerLimit = 0.3f;
    private float hungerGainRatio = 0.01f;

    [SerializeField] private float health = 20f;
    private float healthRadiationDamageRatio = 0.1f;
    private float healthHungerDamageRatio = 0.01f;
    private float healthFoodGainRatio = 0.01f;

    public bool isEating = false;
    private bool isDead = false;

    private int maxChildren = 4;
    private float mutationRatio = 0.2f;

    public List<GameObject> foodSourcesSensed = new List<GameObject>();
    public List<GameObject> radiationAreasSensed = new List<GameObject>();

    public Vector2 movingDirection;

    private bool isIndicatorShowing = false;
    private void CheckCircularSensors() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, creatureData.sensorDistance);
        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag("Food")) {
                foodSourcesSensed.Add(collider.gameObject);
            } else if (collider.CompareTag("Radiation")) {
                radiationAreasSensed.Add(collider.gameObject);
            }
        }
    }

    private Vector2 DetermineMovingDirection() {
        Vector2 moveDirection2D = movingDirection;

        float minFoodDistance = 9999999f;
        int foodSourceIndex = 0;

        if(!(foodSourcesSensed.Count > 0)) {
            return moveDirection2D;
        }

        Debug.Log("direction checked");

        // get closest food source
        for(int i = 0; i < foodSourcesSensed.Count; i++) {
            Vector2 tempVector = transform.position - foodSourcesSensed[i].transform.position;
            float tempDistance = Vector2.Distance(tempVector, transform.position);
            if(tempDistance < minFoodDistance) {
                minFoodDistance = tempDistance;
                foodSourceIndex = i;
            }            
        }

        //float minRadiationDistance = 9999999f;
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
            moveDirection2D = foodSourcesSensed[foodSourceIndex].transform.position;
        //} else {
        //    moveDirection2D = radiationAreasSensed[radiationSourceIndex].transform.position;
        //}     

        foodSourcesSensed.Clear();
        radiationAreasSensed.Clear();

        return moveDirection2D;
    }

    private void Eat() {
        if (hunger >= hungerLimit) {
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
    
    private void GetStarveDamage() {
        if (health > 0) {
            health -= healthHungerDamageRatio * hunger;
        } else {
            health = 0;
            isDead = true;
        }
    }

    private void Reproduce() {
        // code to spawn new child
        for(int i = 0; i < UnityEngine.Random.Range(1, maxChildren+1); i++) {
            SpawnCreature();
        }
    }

    void SpawnCreature() {
        Vector3 spawnPosition = transform.position;
        GameObject spawnedCreature = Instantiate(gameObject, spawnPosition, Quaternion.identity);
        spawnedCreature.transform.SetParent(transform.parent);
        
        Creature creatureComponent = spawnedCreature.GetComponent<Creature>();
        if(creatureComponent == null) {
            Debug.Log("Couldnt get creature component reproduction");
            return;
        }

        float _maxHealth = UnityEngine.Random.Range(creatureData.maxHealth - creatureData.maxHealth * mutationRatio, creatureData.maxHealth + creatureData.maxHealth * mutationRatio);
        float _maxAge = UnityEngine.Random.Range(creatureData.maxAge - creatureData.maxAge * mutationRatio, creatureData.maxAge + creatureData.maxAge * mutationRatio);
        float _moveSpeed = UnityEngine.Random.Range(creatureData.moveSpeed - creatureData.moveSpeed * mutationRatio, creatureData.moveSpeed + creatureData.moveSpeed * mutationRatio);
        float _sensorDistance = UnityEngine.Random.Range(creatureData.sensorDistance - creatureData.sensorDistance * mutationRatio, creatureData.sensorDistance + creatureData.sensorDistance * mutationRatio);

        creatureComponent.creatureData.SetCreatureData(_maxHealth, _maxAge, _moveSpeed, _sensorDistance);
    }

    private void MoveCreature() {
        if (isEating) {
            parentRigibody2D.velocity = Vector2.zero;
            return;
        }

        parentRigibody2D.velocity = movingDirection * creatureData.moveSpeed;
    }

    private static float FitnessLifeGraph(float age, float fitnessGene, float s) {
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

        SpawnCreature();
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

        movingDirection = Vector2.one;
    }

    private void Update() {

        // Death Update
        if (isDead) {
            //Destroy(gameObject);
        }

        // Hunger Update
        hunger += hungerGainRatio * Time.deltaTime; // adjust to time it
        if (hunger > hungerLimit) {
            GetStarveDamage();
        }

        // Age Update
        age += 1f * Time.deltaTime; // adjust to time it

        // Fitness Update
        fitness = FitnessLifeGraph(age, fitnessGene, 1f);

        // Reproduce Update
        if (fitness == fitnessGene && health >= 0.5 * creatureData.maxHealth) {
            Reproduce();
        }

        // Sensor Update
        CheckCircularSensors();

        // Determine direction
        movingDirection = DetermineMovingDirection();

        // Move Update
        MoveCreature();

        // Update UI
        UpdateCreatureIndicatorData(transform);

    }

    // Other updates on collisions
    public void Collided(string name) {
        if (name == "Food" && hunger > hungerLimit) {
            isEating = true;
            Debug.Log("isEating");
            Eat();
        } else if (name == "Radiation") {
            GetRadiationDamaged();
        }
    }

}