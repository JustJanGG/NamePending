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
    public void DefaultBlueCircuitOnTriggerEnter2D(Collider2D collision, GameObject prefabOf)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<BlueCircuit>().Hit(collision.gameObject, reducedList, damage);
        }
    }
}
