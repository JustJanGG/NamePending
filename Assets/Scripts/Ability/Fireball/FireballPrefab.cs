using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballPrefab : AbilityPrefab, IProjectile
{
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }
    public int pierceCount { get; set; }
    public int chainCount { get; set; }
    public List<GameObject> alreadyHitEnemies { get; set; }
    public GameObject projectile { get; set; }

    void Start()
    {
        ((IProjectile)this).InitiateProjectile(this.gameObject);
        Destroy(gameObject, prefabOf.GetComponent<Ability>().lifetime);
    }

    void Update()
    {
        ((IProjectile)this).DefaultProjectileMovement(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DefaultOnTriggerEnter2D(collision);
        ((IProjectile)this).ProjectileHit(gameObject, collision.GameObject());
    }
}
