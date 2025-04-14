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
        //int rng = Random.Range(1, 101);
        //Debug.Log("ProcChance: " + procChance);
        //Debug.Log("rng: " + rng + "Proc chance: " + procChance * 100);
        //if (rng <= procChance * 100)
        //{
        //    return true;
        //}
        if (Random.Range(1, 101) <= procChance * 100)
        {
            return true;
        }
        return false;
    }

    public override void Activate()
    {
        //Not supposed to be called
    }
    public override void Hit(GameObject enemy)
    {
        //Not supposed to be called
    }
    public abstract void Hit(GameObject enemy, List<BlueCircuit> reducedList);
    public abstract void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, float[] damage);
}
