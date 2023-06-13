using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefabDataSaver : MonoBehaviour
{
    public GameObject prefab;

    
    
    private string jsonFilePath;

    private void Start()
    {
        // File path to save the json file
        jsonFilePath = Application.dataPath + "/Resources/prefabData.json";
    }

    public void SavePrefabData()
    {
        // Write frefab tag to console
        Debug.Log(prefab.tag);

        List<TransformDataSaver> prefabDataList = new List<TransformDataSaver>();
        GameObject[] prefabClones = GameObject.FindGameObjectsWithTag(prefab.tag);

        foreach (GameObject clone in prefabClones)
        {
            //
            Debug.Log(clone.transform.position.x);

            prefabDataList.Add(new TransformDataSaver(clone.transform));
        }
        // 
        Debug.Log(prefabDataList[0].position.x);

        // 
        Debug.Log(prefabDataList.ToArray()[0].position.x);

        string jsonString = JsonHelper.ToJson(prefabDataList.ToArray());
        Debug.Log(jsonString);

        File.WriteAllText(jsonFilePath, jsonString);
    }

    public void GoToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}

[System.Serializable]
public class JsonHelper
{
    public static string ToJson<T>(T[] array)
    {
        // Get position, rotation, and scale of each object in the array
        // and save it to a json file
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        //
        Debug.Log(wrapper.Items[0].ToString());
        return JsonUtility.ToJson(wrapper);
    }

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}