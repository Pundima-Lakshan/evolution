using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestingSaveAge : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] Creature creature;

    private void Awake() {
        m_TextMeshProUGUI.text = creature.age.ToString();
    }

    private void Update() {
        m_TextMeshProUGUI.text = creature.age.ToString();
    }
}
