using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour {

    [SerializeField] private bool isControllable = false;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rigibody2D;

    CreatureStats stats;

    // Start is called before the first frame update
    void Start() {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    int count = 0;

    // Update is called once per frame
    void Update() {
        MoveParameters moveParameters = new MoveParameters();

        if (count > 30) {
            Move(moveParameters);
            count = 0;
        } else {
            count++;
        }
    }

    private void UpdateSensesOfCreature() {

    }

    private void UpdateCreatureParameters() {

    }

    void Move(MoveParameters moveParameters) {
        if (isControllable) {
            //TODO input code
        } else {
            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(-10, 10);
            Vector2 moveDirection2D = new Vector2(randomX, randomY);

            rigibody2D.velocity = moveDirection2D * moveSpeed;

            //moveDirection2D.Normalize();
            //transform.position += new Vector3(moveDirection2D.x * moveSpeed * Time.deltaTime, moveDirection2D.y * moveSpeed * Time.deltaTime, 0);
        }
     }
}
