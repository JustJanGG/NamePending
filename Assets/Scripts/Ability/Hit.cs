using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hit 
{
    private GameObject enemy;
    private EnemyStats enemyStats;
    private Ability ability;
    private List<BlueCircuit> circuits;

    public Hit(GameObject enemy, Ability ability)
    {
        this.enemy = enemy;
        enemyStats = enemy.GetComponent<EnemyStats>();
        this.ability = ability;
        circuits = ability.GetBlueCircuits();

        enemyStats.TakeDamage(ability.DealDamage());

        foreach (var circuit in circuits)
        {
            
        }
    }
}
