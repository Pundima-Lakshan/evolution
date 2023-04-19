using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSense : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2DSense;

    private void Start() {
        circleCollider2DSense = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision");
    }
}
