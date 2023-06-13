using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CreatureWithNeuralNetwork : MonoBehaviour, IPointerClickHandler {

    private const float sensorDistance = 5f;
    private const float moveSpeed = 5f;
    private const float maxHealth = 10f;
    private const float maxAge = 10f;
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

    [SerializeField] private Rigidbody2D rigibody2D;

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Food") {
            //collision.gameObject.GetComponent<Food>().Eat();
            Eat();
        }
        else if(collision.gameObject.tag == "Radiation") {
            GetRadiationDamaged();
        }
    }

    private void CheckSensors() {
        Vector3 forward = (transform.forward);
        Vector3 rightDiagonal = (transform.forward + transform.right);
        Vector3 leftDiagonal = (transform.forward - transform.right);

        Ray r = new Ray(transform.position, forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(r, out raycastHit, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
            }
        }

        r.direction = rightDiagonal;
        if (Physics.Raycast(r, out raycastHit, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
            }
        }

        r.direction = leftDiagonal;
        if (Physics.Raycast(r, out raycastHit, sensorDistance)) {
            if (raycastHit.collider.gameObject.tag == "Food") {
                Debug.Log("Food detected at " + sensorDistance);
            } else if (raycastHit.collider.gameObject.tag == "Radiation") {
                Debug.Log("Radiation detected at " + sensorDistance);
            } else {
                Debug.Log("Nothing detected at " + sensorDistance);
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
        // rotation angle to vector 2D direction
        Vector2 moveDirection2D = new Vector2(Convert.ToSingle(Math.Cos(rotationAngle)), Convert.ToSingle(Math.Sin(rotationAngle)));
        rigibody2D.velocity = moveDirection2D * moveSpeed;
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
        CheckSensors();

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