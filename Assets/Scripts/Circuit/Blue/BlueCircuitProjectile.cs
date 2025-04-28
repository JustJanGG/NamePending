using System.Collections.Generic;
using UnityEngine;

public class BlueCircuitProjectile : AbilityPrefab, IProjectile, IBlueCircuitPrefab
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<BlueCircuit>().Hit(collision.gameObject, reducedList, damage);
        }
    }
}
