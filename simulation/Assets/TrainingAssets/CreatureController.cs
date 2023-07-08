using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NNet))]
public class CreatureController : MonoBehaviour
{
    private Vector3 startPosition, startRotation;
    private Vector2 moveDirection2D;
    public NNet network;

    //[Range(-1f, 1f)]
    public float ac, t;

    public float timeSinceStart = 0f;
    public float timeMultiplier = 2f;

    [Header("Fitness")]
    public float overallFitness = 0f;
    public float distanceMultipler = 1.4f;
    public float avgSpeedMultiplier = 0.2f;
    public float sensorMultiplier = 0.1f;

    public float health = 0f;
    public float healthMultiplier = 5f;
    public float foodMultiplier = 0.1f;
    public float radioactiveMultiplier = 0.1f;

    [Header("Network Options")]
    public int LAYERS = 2;
    public int NEURONS = 20;

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private float aSensor, bSensor, cSensor;
    private float aSensorT, bSensorT, cSensorT;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        network = GetComponent<NNet>();
        //network.Initialise(LAYERS, NEURONS);
    }
    public void ResetWithNetwork(NNet net)
    {
        network = net;
        Reset();
    }

    public void Reset()
    {
        //network.Initialise(LAYERS, NEURONS);
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }

    public void Collided(string tagName)
    {
        if (tagName == "Radioactive")
        {
            //Debug.Log("Radioactive");
            health = -1 * radioactiveMultiplier;
        }
        else if (tagName == "Food")
        {
            //Debug.Log("Food");
            health = foodMultiplier;
        }
        else if(tagName == "Border")
        {
            //Debug.Log("Border");
            Death();
        }
        
        
        

    }

    private void FixedUpdate()
    {

        InputSensors();
        lastPosition = transform.position;

        (ac, t) = network.RunNetwork(aSensor, aSensorT, bSensor, bSensorT, cSensor, cSensorT);

        MoveCreature(t);

        timeSinceStart += Time.deltaTime;

        CalculateFitness();

        //a = 0;
        //t = 0;
        //health -= Time.deltaTime * timeMultiplier;

    }

    private void Death()
    {
        GameObject.FindObjectOfType<GeneticManager>().Death(overallFitness, network);
    }

    private void CalculateFitness()
    {

        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        //avgSpeed = totalDistanceTravelled / timeSinceStart;

        //overallFitness = (totalDistanceTravelled * distanceMultipler) + (avgSpeed * avgSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier) + (health*healthMultiplier);
        //overallFitness = (totalDistanceTravelled * distanceMultipler) + (health * healthMultiplier);
        overallFitness += (health * healthMultiplier)/*+ (totalDistanceTravelled * distanceMultipler)*/;
        health = 0f;

        if (timeSinceStart > 15)
        {
            //Reset();
            Death();
        }

        //if (totalDistanceTravelled >= 1000)
        //{
        //    //Saves network to a JSON
        //    Death();
        //}

    }

    private void InputSensors()
    {

        //Vector3 a = (transform.up + transform.right).normalized;
        //Vector3 b = (transform.up).normalized;
        //Vector3 c = (transform.up - transform.right).normalized;

        Vector3 a = RotateDirection(moveDirection2D, 45f).normalized;
        Vector3 b = moveDirection2D.normalized;
        Vector3 c = RotateDirection(moveDirection2D, -45f).normalized;

        float sensorDistance = 5f;
        //Ray r = new Ray(transform.position, a);
        //RaycastHit hit;
        RaycastHit2D[] raycastHits;
        
        raycastHits = Physics2D.RaycastAll(transform.position, a, sensorDistance);
        foreach (RaycastHit2D raycastHit in raycastHits) 
        {
            Debug.DrawLine(transform.position, transform.position + a.normalized * sensorDistance, Color.cyan);
            if (raycastHit.collider.gameObject.tag == "Food")
            {
                //Debug.Log("Food detected at " + sensorDistance);
                aSensor = raycastHit.distance;
                aSensorT = 0.5f;
            }
            else if (raycastHit.collider.gameObject.tag == "Radiation")
            {
                //Debug.Log("Radiation detected at " + sensorDistance);
                aSensor = raycastHit.distance;
                aSensorT = 1f;
            }
            else
            {
                //Debug.Log("Nothing detected at " + sensorDistance);
                aSensor = 1f;
                aSensorT = 0f;
            }
        }

        //r.direction = b;

        raycastHits = Physics2D.RaycastAll(transform.position, b, sensorDistance);
        foreach (RaycastHit2D raycastHit in raycastHits)
        {
            Debug.DrawLine(transform.position, transform.position + b.normalized * sensorDistance, Color.cyan);
            if (raycastHit.collider.gameObject.tag == "Food")
            {
                //Debug.Log("Food detected at " + sensorDistance);
                bSensor = raycastHit.distance;
                bSensorT = 0.5f;
            }
            else if (raycastHit.collider.gameObject.tag == "Radiation")
            {
                //Debug.Log("Radiation detected at " + sensorDistance);
                bSensor = raycastHit.distance;
                bSensorT = 1f;
            }
            else
            {
                //Debug.Log("Nothing detected at " + sensorDistance);
                bSensor = 1f;
                bSensorT = 0f;
            }
        }

        //r.direction = c;

        raycastHits = Physics2D.RaycastAll(transform.position, c, sensorDistance);
        foreach (RaycastHit2D raycastHit in raycastHits)
        {
            Debug.DrawLine(transform.position, transform.position + c.normalized * sensorDistance, Color.cyan);
            if (raycastHit.collider.gameObject.tag == "Food")
            {
                //Debug.Log("Food detected at " + sensorDistance);
                cSensor = raycastHit.distance;
                cSensorT = 0.5f;
            }
            else if (raycastHit.collider.gameObject.tag == "Radiation")
            {
                //Debug.Log("Radiation detected at " + sensorDistance);
                cSensor = raycastHit.distance;
                cSensorT = 1f;
            }
            else
            {
                //Debug.Log("Nothing detected at " + sensorDistance);
                cSensor = 1f;
                cSensorT = 0f;
            }
        }

    }
    /*
    private Vector2 inp;
    public void MoveCreature(float v, float h)
    {
        inp = Vector2.Lerp(Vector2.zero, new Vector2(0, v * 11.4f), 0.02f);
        inp = transform.TransformDirection(inp);
        transform.position += inp;

        transform.eulerAngles += new Vector2(0, (h * 90) * 0.02f);
    }*/
    [SerializeField] private Rigidbody2D parentRigibody2D;
    [SerializeField] private GameObject sensorGameObject;
    [SerializeField] float moveSpeed = 2f;
    private void MoveCreature(float rotationAngle)
    {
        float remapedRotatingAngle = MapValue(rotationAngle);
        remapedRotatingAngle = DegreesToRadians(remapedRotatingAngle);

        // rotation angle to vector 2D direction for creature
        moveDirection2D = new Vector2(Convert.ToSingle(Math.Cos(remapedRotatingAngle)), Convert.ToSingle(Math.Sin(remapedRotatingAngle)));
        parentRigibody2D.velocity = moveDirection2D * moveSpeed;

        // rotating the sensor
        //sensorGameObject.transform.eulerAngles += new Vector3(0, 0, (remapedRotatingAngle * 90) * 0.2f);
    }

    // Function to map a value from -1 to +1 to the range of 0 to 360
    private float MapValue(float value)
    {
        return Remap(value, -1f, 1f, 0f, 360f);
    }

    // Linear mapping function
    private float Remap(float value, float originalMin, float originalMax, float newMin, float newMax)
    {
        float normalizedValue = Mathf.InverseLerp(originalMin, originalMax, value);
        float remappedValue = Mathf.Lerp(newMin, newMax, normalizedValue);
        return remappedValue;
    }

    // Function to convert angle from degrees to radians
    private float DegreesToRadians(float degrees)
    {
        return degrees * Mathf.Deg2Rad;
    }

    private Vector2 RotateDirection(Vector2 originalDirection, float rotatingAngle)
    {
        // Convert the original direction to an angle in radians
        float originalAngle = Mathf.Atan2(originalDirection.y, originalDirection.x);

        // Calculate the new angle by adding the desired clockwise angle in degrees
        float newAngle = originalAngle + Mathf.Deg2Rad * rotatingAngle;

        // Calculate the new direction vector
        Vector2 rotatedDirection = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));

        return rotatedDirection;
    }
}

