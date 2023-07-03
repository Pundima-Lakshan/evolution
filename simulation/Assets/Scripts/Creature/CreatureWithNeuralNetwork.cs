using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CreatureWithNeuralNetwork : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private Rigidbody2D parentRigibody2D;
    [SerializeField] private GameObject sensorGameObject;
 
    public float maxHealth = 10f;
    public float maxAge = 10f;
    public float moveSpeed = 5f;
    public float sensorDistance = 5f;    
    
    private const float fitnessGene = 0.5f; // 0 to 1

    private float age = 0f;
    private float fitness = 0f;

    private float hunger = 0f;
    private float hungerFoodReduce = 0.1f;

    private float health = 0f;
    private float healthRadiationDamage = 0.1f;
    private float healthHungerDamage = 0.1f;
    private float healthFoodGain = 0.1f;

    private bool isDead = false;

    private float forwardSensorDetection, rightDiagonalSensorDetection, leftDiagonalSensorDetection; // 1 - 0.5 - 0
    private float forwardSensorDistance, rightDiagonalSensorDistance, leftDiagonalSensorDistance;

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Food") {
            //collision.gameObject.GetComponent<Food>().Eat();
            Eat();
        }
        else if(collision.gameObject.tag == "Radiation") {
            GetRadiationDamaged();
        }
    }

    private void CheckRaySensors() {
        Vector2 forward = (transform.up);
        Vector2 rightDiagonal = (transform.up + transform.right);
        Vector2 leftDiagonal = (transform.up - transform.right);

        RaycastHit2D raycastHit;

        if (raycastHit = Physics2D.Raycast(transform.position, forward, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
                forwardSensorDistance = raycastHit.distance;
                forwardSensorDetection = 0.5f;
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
                forwardSensorDistance = raycastHit.distance;
                forwardSensorDetection = 1f;
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
                forwardSensorDistance = 1f;
                forwardSensorDetection = 0f;
            }
        }

        if (raycastHit = Physics2D.Raycast(transform.position, rightDiagonal, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
                rightDiagonalSensorDistance = raycastHit.distance;
                rightDiagonalSensorDetection = 0.5f;
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
                rightDiagonalSensorDistance = raycastHit.distance;
                rightDiagonalSensorDetection = 1f;
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
                rightDiagonalSensorDistance = raycastHit.distance;
                rightDiagonalSensorDetection = 0f;
            }
        }

        if (raycastHit = Physics2D.Raycast(transform.position, leftDiagonal, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
                leftDiagonalSensorDistance = raycastHit.distance;
                leftDiagonalSensorDetection = 0.5f;
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
                leftDiagonalSensorDistance = raycastHit.distance;
                leftDiagonalSensorDetection = 1f;
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
                leftDiagonalSensorDistance = raycastHit.distance;
                leftDiagonalSensorDetection = 0f;
            }
        }
    }

    private void Eat() {
        if (hunger >= 0) {
            hunger -= hungerFoodReduce;
        } else {
            hunger = 0;
        }

        if(health < maxHealth) {
            health += healthFoodGain;
        } else {
            health = maxHealth;
        }
    }

    private void GetRadiationDamaged() {
        if(health > 0) {
            health -= healthRadiationDamage;
        } else {
            health = 0;
            isDead = true;
        }
    }

    private void GetStarveDamage() {
        if (health > 0) {
            health -= healthHungerDamage;
        } else {
            health = 0;
            isDead = true;
        }        
    }

    private void Reproduce() {
        // code to spawn new child
    }

    private void MoveCreature(float rotationAngle) {
        // rotation angle to vector 2D direction for creature
        Vector2 moveDirection2D = new Vector2(Convert.ToSingle(Math.Cos(rotationAngle)), Convert.ToSingle(Math.Sin(rotationAngle)));
        parentRigibody2D.velocity = moveDirection2D * moveSpeed;

        // rotating the sensor
        sensorGameObject.transform.eulerAngles += new Vector3(0, 0, (rotationAngle * 90) * 0.02f);

        Debug.DrawRay(sensorGameObject.transform.position, moveDirection2D, Color.red, 0.1f, false);
    }

    private float NeuralNetworkUpdate() {
        // code to update neural network
        return 0.5f;
    }

    private void Update() {
        
        // Death Update
        if (isDead) {
            Destroy(gameObject);
        }

        // Hunger Update
        hunger += Time.deltaTime; // adjust to time it
        if (hunger > 0) {
            GetStarveDamage();
        }

        // Age Update
        age += Time.deltaTime; // adjust to time it

        // Fitness Update
        fitness = FitnessLifeGraph(age, fitnessGene, 1f);

        // Reproduce Update
        if (fitness == fitnessGene && health >= 0.5 * maxHealth) {
            Reproduce();
        }

        // Sensor Update
        CheckRaySensors();

        // Neural Network Update
        float rotationAngle = NeuralNetworkUpdate();

        // Move Update
        MoveCreature(rotationAngle);
    }

    private static float FitnessLifeGraph(float age, float fitnessGene, float s) {
        double coefficient = 1 / (s * Math.Sqrt(2 * Math.PI));
        double exponent = -Math.Pow(age - fitnessGene, 2) / (2 * Math.Pow(s, 2));
        return Convert.ToSingle(coefficient * Math.Exp(exponent));
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
}