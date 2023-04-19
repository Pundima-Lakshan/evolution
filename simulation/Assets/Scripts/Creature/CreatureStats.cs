using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CreatureStats {
    float health;
    float energy;
    float size;
    float weight;

    float defence;
    float attack;
    float speed;

    float senseRange;

    float hostility;
    float reproduction;
}

struct MoveParameters {
    float speedMultipler;
    int direction;
}
