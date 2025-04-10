using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hit 
{
    private GameObject enemy;
    private Ability ability;
    private List<BlueCircuit> circuits;

    public Hit(GameObject enemy, Ability ability)
    {
        this.enemy = enemy;
        this.ability = ability;
        circuits = ability.GetBlueCircuits();
    }
}
