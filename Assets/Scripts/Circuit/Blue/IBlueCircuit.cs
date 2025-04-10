using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IBlueCircuit : ICircuit
{
    float procCoefficient { get; set; }
    float procChance { get; set; }
    Ability socketedAbility { get; set; }
    List<RedCircuit> redCircuits { get; set; }


    public bool Proc(float procCoefficient)
    {
        if (Random.Range(1, 101) <= procChance * 100)
        {
            return true;
        }
        return false;
    }
}
