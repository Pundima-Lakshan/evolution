using UnityEngine;
using System;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class DataSave : MonoBehaviour
{
    public DataSave instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveToJson(string savingData)
    {
        string saveData = JsonUtility.ToJson(savingData);
        string filePath = Application.persistentDataPath + "/saveData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, saveData);
        Debug.Log("Saved to " + filePath);
    }

    public string LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/saveData.json";
        string savedData = System.IO.File.ReadAllText(filePath);
        Debug.Log("Loaded from " + filePath);
        string savedJsonData = JsonUtility.FromJson<string>(savedData);
        return savedJsonData;
    }
}
