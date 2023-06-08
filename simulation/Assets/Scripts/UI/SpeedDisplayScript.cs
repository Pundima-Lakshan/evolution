using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDisplayScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedDisplayText;

    private void Start() {
    }

    // Update is called once per frame
    public void UpdateString()
    {
        string displayText = "Speed: " + Time.timeScale + "X";
        if (speedDisplayText != null)
            speedDisplayText.SetText(displayText);
        else
            Debug.Log("speedDisplayText is null");
    }
}
