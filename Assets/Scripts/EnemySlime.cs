using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyBaseClass
{

    protected override void BaseStats()
    {
        // Base stats
        enemyHealth = 3;
        enemyName = "Slime";
        baseAttack = 1;
        moveSpeed = 1f;
        chaseRadius = 4f;
        attackRadius = 1f;
        attackSpeed = 2f;
    }
}
