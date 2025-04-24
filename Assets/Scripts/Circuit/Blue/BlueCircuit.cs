using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlueCircuit : Ability, ICircuit
{
    public int id { get; set; }
    public string circuitName { get; set; }
    public string circuitDescription { get; set; }
    public CircuitType circuitType { get; set; }

    [Header("Blue Circuit Stats")]
    public float procChance;

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

    public override void Activate()
    {
        //Not supposed to be called
        Debug.LogError("BlueCircuit Activate() called without enemy");
    }
    public override void Hit(GameObject enemy)
    {
        //Not supposed to be called
        Debug.LogError("BlueCircuit Hit() called without reduced list");
    }
    public void Hit(GameObject enemy, List<BlueCircuit> reducedList)
    {
        Hit hit = new(enemy, this, reducedList, DealDamage());
    }
    public abstract void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage);
}
