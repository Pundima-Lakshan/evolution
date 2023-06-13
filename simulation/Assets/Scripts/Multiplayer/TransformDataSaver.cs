using System;
using UnityEngine;

[Serializable]
public class TransformDataSaver : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public TransformDataSaver(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }
}