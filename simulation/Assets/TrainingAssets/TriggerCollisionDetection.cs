using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CreatureController creatureController = collision.GetComponent<CreatureController>();
        if(creatureController != null)
        {
            creatureController.Collided(this.gameObject.tag.ToString());
        }
    }
}
