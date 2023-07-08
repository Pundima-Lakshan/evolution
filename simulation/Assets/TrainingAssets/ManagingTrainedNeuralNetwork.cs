using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class ManagingTrainedNeuralNetwork : MonoBehaviour
{
    public static ManagingTrainedNeuralNetwork instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public Matrix<float> inputLayerTrained = Matrix<float>.Build.Dense(1, 6);
    public List<Matrix<float>> hiddenLayersTrained = new List<Matrix<float>>();
    public Matrix<float> outputLayerTrained = Matrix<float>.Build.Dense(1, 2);
    public List<Matrix<float>> weightsTrained = new List<Matrix<float>>();
    public List<float> biasesTrained = new List<float>();

    public void LoadTheTrainedData(NNet trainedNeuralNetwork)
    {
        inputLayerTrained = trainedNeuralNetwork.inputLayer;
        hiddenLayersTrained = trainedNeuralNetwork.hiddenLayers;
        outputLayerTrained = trainedNeuralNetwork.outputLayer;
        weightsTrained = trainedNeuralNetwork.weights;
        biasesTrained = trainedNeuralNetwork.biases;
        
    }

}
