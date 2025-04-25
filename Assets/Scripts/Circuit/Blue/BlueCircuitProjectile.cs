using System.Collections.Generic;
using UnityEngine;

public class BlueCircuitProjectile : Projectile, IBlueCircuitPrefab
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<BlueCircuit>().Hit(collision.gameObject, reducedList, damage);
        }
    }
}
