using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LostScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = GameManager.instance.GetGameTime().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
