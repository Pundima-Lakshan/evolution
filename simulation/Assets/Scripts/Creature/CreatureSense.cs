using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSense : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2DSense;

    private void Start() {
        circleCollider2DSense = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log(collider.name.ToString());
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //Debug.Log(collision.name.ToString());
    }
}
