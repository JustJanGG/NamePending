using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hit
{
    private EnemyStats enemyStats;
    private List<BlueCircuit> reducedList;

    public Hit(GameObject enemy, Ability ability, List<BlueCircuit> blueCircuits, float[] damage)
    {
        reducedList = new List<BlueCircuit>();
        enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.TakeDamage(damage);
        List<BlueCircuit> triggered = new List<BlueCircuit>();
        if (blueCircuits != null && blueCircuits.Count > 0)
        {
            foreach (BlueCircuit circuit in blueCircuits)
            {
                if (circuit.Proc(ability.procCoefficiant))
                {
                    triggered.Add(circuit);
                }
                else
                {
                    reducedList.Add(circuit);
                }
            }
            foreach (var circuit in triggered)
            {
                ability.ProcBlueCircuit(circuit, enemy, reducedList, ability.DealDamage());
            }

        }
    }
}
