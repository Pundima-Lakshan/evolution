using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDisplayScript : MonoBehaviour
{
    private static TextMeshProUGUI speedDisplayText;

    private void Start() {
        speedDisplayText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void updateString()
    {
        string displayText = "Speed: " + Time.timeScale + "X";
        if (speedDisplayText != null)
            speedDisplayText.SetText(displayText);
        else
            Debug.Log("speedDisplayText is null");
    }
}
