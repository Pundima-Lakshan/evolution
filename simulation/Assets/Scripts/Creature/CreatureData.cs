using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureData
{
    public float age;

    public CreatureData(Creature creature) {
        age = creature.age;
    }
}
