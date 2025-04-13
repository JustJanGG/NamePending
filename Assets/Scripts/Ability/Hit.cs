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
        List<BlueCircuit> triggered = new List<BlueCircuit>();
        if (blueCircuits != null)
        {
            //Debug.Log("blueCircuit not null");
            foreach (var circuit in blueCircuits)
            {
                Debug.Log("Checking for proc");
                if (circuit.Proc(ability.procCoefficiant))
                {
                    Debug.Log("Succesfull proc");
                    triggered.Add(circuit);
                }
            }
            //Debug.Log(triggered);
            foreach (var circuit in triggered)
            {
                ability.ProcBlueCircuit(this, circuit, enemy);
            }

        }
    }
}
