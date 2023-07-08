using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.IO;
using Newtonsoft.Json;

public class NNetGame : MonoBehaviour
{
    public Matrix<float> inputLayer = Matrix<float>.Build.Dense(1, 6);
    public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();
    public Matrix<float> outputLayer = Matrix<float>.Build.Dense(1, 2);
    public List<Matrix<float>> weights = new List<Matrix<float>>();
    public List<float> biases = new List<float>();

    private Vector2 moveDirection2D;
    public float aSensor,aSensorT,bSensor,bSensorT,cSensor,cSensorT;
    public float ac,t;

    public (float, float) RunNetwork(float a, float b, float c, float d, float e, float f)
    {
        inputLayer[0, 0] = a;
        inputLayer[0, 1] = b;
        inputLayer[0, 2] = c;
        inputLayer[0, 3] = d;
        inputLayer[0, 4] = e;
        inputLayer[0, 5] = f;

        inputLayer = inputLayer.PointwiseTanh();

        hiddenLayers[0] = ((inputLayer * weights[0]) + biases[0]).PointwiseTanh();
        //hiddenLayers[0] = inputLayer;

        for (int i = 1; i < hiddenLayers.Count; i++)
        {
            hiddenLayers[i] = ((hiddenLayers[i - 1] * weights[i]) + biases[i]).PointwiseTanh();
        }

        outputLayer = ((hiddenLayers[hiddenLayers.Count - 1] * weights[weights.Count - 1]) + biases[biases.Count - 1]).PointwiseTanh();

        //First output is acceleration and second output is steering
        return (Sigmoid(outputLayer[0, 0]), (float)Math.Tanh(outputLayer[0, 1]));
    }

    private void InputSensors()
    {

        //Vector3 a = (transform.up + transform.right).normalized;
        //Vector3 b = (transform.up).normalized;
        //Vector3 c = (transform.up - transform.right).normalized;

        Vector3 a = RotateDirection(moveDirection2D, 45f).normalized;
        Vector3 b = moveDirection2D.normalized;
        Vector3 c = RotateDirection(moveDirection2D, -45f).normalized;

        float sensorDistance = 3f;
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

    [SerializeField] private Rigidbody2D parentRigibody2D;
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

    private float Sigmoid(float s)
    {
        return (1 / (1 + Mathf.Exp(-s)));
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise(2, 15);
        weights = ManagingTrainedNeuralNetwork.instance.weightsTrained;
        biases = ManagingTrainedNeuralNetwork.instance.biasesTrained;
        RandomiseWeights();

        moveInRandomDirection();
    }

    float creatureTime = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        InputSensors();

        if (creatureTime < 50)
        {
            (ac, t) = RunNetwork(aSensor, aSensorT, bSensor, bSensorT, cSensor, cSensorT);
            MoveCreature(t);
        }
        else
        {
            moveInRandomDirection();
            creatureTime = 0;
        }
    }

    private void Update()
    {
        creatureTime += Time.deltaTime;

    }

    public void Initialise(int hiddenLayerCount, int hiddenNeuronCount)
    {

        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();
        weights.Clear();
        biases.Clear();

        for (int i = 0; i < hiddenLayerCount + 1; i++)
        {

            Matrix<float> f = Matrix<float>.Build.Dense(1, hiddenNeuronCount);

            hiddenLayers.Add(f);

            biases.Add(UnityEngine.Random.Range(-1f, 1f));

            //WEIGHTS
            if (i == 0)
            {
                Matrix<float> inputToH1 = Matrix<float>.Build.Dense(6, hiddenNeuronCount);
                weights.Add(inputToH1);
            }

            Matrix<float> HiddenToHidden = Matrix<float>.Build.Dense(hiddenNeuronCount, hiddenNeuronCount);
            weights.Add(HiddenToHidden);

        }

        Matrix<float> OutputWeight = Matrix<float>.Build.Dense(hiddenNeuronCount, 2);
        weights.Add(OutputWeight);
        biases.Add(UnityEngine.Random.Range(-1f, 1f));

        //RandomiseWeights();

    }

    public void RandomiseWeights()
    {
        for (int i = 0; i < weights.Count; i++)
        {
            for (int x = 0; x < weights[i].RowCount; x++)
            {
                for (int y = 0; y < weights[i].ColumnCount; y++)
                {

                    weights[i][x, y] += UnityEngine.Random.Range(-0.01f, 0.01f);

                }
            }
        }
    }

    private float moveDirectionSetCount = 0f;
    private void moveInRandomDirection()
    {
        moveDirection2D = new Vector2(UnityEngine.Random.Range(-100, +100 + 1), UnityEngine.Random.Range(-100, +100 + 1)).normalized;
        parentRigibody2D.velocity = moveDirection2D * moveSpeed;
        moveDirectionSetCount = 0;
        
        return;
    }
}
