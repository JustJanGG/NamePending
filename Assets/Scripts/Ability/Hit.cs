using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hit 
{
    private EnemyStats enemyStats;
    //private float[] damage;

    public Hit(GameObject enemy, Ability ability, List<BlueCircuit> blueCircuits, float[] damage)
    {
        enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.TakeDamage(damage);

        foreach (var circuit in blueCircuits)
        {
            if (circuit.Proc(ability.procCoefficiant))
            {

            }
        }
    }
}
