using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Creature") {
            Creature creature = collision.gameObject.GetComponent<Creature>();
            if (creature == null) {
                Debug.Log("creature not found in trigger radiation");
                return;
            }
            creature.Collided("Radiation");
        }
    }
}
