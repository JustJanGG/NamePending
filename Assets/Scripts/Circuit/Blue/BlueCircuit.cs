using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlueCircuit : Ability, ICircuit
{
    public int id { get; set; }
    public string circuitName { get; set; }
    public string circuitDescription { get; set; }
    public CircuitType circuitType { get; set; }
    float procChance;
    Ability socketedInAbility;
    List<RedCircuit> redCircuits;
    public bool Proc(float procCoefficient)
    {
        if (Random.Range(1, 101) <= procChance * 100)
        {
            return true;
        }
        return false;
    }
}
