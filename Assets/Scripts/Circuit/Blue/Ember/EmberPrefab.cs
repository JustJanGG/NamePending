using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmberPrefab : AbilityPrefab, IProjectile, IBlueCircuitPrefab
{
    [HideInInspector]
    public Transform target;
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }
    public int pierceCount { get; set; }
    void Start()
    {
        ((IProjectile)this).InitiateProjectile();
        direction = target.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        ((IProjectile)this).DefaultProjectileMovement(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ((IBlueCircuitPrefab)this).DefaultBlueCircuitOnTriggerEnter2D(collision, prefabOf);
        ((IProjectile)this).ProjectileHit(gameObject);
    }
}
