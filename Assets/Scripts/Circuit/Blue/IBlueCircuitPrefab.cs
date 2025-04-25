using System.Collections.Generic;
using UnityEngine;

public interface IBlueCircuitPrefab
{
    List<BlueCircuit> reducedList { get; set; }
    Dictionary<DamageType, float> damage { get; set; }
    public void PassList(List<BlueCircuit> reducedList)
    {
        this.reducedList = reducedList;
    }
    public void PassDamage(Dictionary<DamageType, float> damage)
    {
        this.damage = damage;
    }
}
