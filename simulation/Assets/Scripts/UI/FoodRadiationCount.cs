using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodRadiationCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if(text.name == "FoodCount")
            text.text = GameManager.instance.GetFoodSourceCount().ToString("0");
        else if(text.name == "RadiationCount")
            text.text = text.text = GameManager.instance.GetRadiationZoneCount().ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
