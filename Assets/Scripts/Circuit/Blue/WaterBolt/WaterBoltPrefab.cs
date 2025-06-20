using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBoltPrefab : AbilityPrefab, IProjectile, IBlueCircuitPrefab
{
    [HideInInspector]
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }
    public int pierceCount { get; set; }
    public int chainCount { get; set; }
    public List<GameObject> alreadyHitEnemies { get; set; }
    public GameObject projectile { get; set; }

    void Start()
    {
        ((IProjectile)this).InitiateProjectile(this.gameObject);
        GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        ((IProjectile)this).DefaultProjectileMovement(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.PlayOneShot(audioClips[2]);
        transform.Find("Particle Explosion").gameObject.SetActive(true);
        GetComponentInChildren<ParticleSystem>().Play();
        ((IBlueCircuitPrefab)this).DefaultBlueCircuitOnTriggerEnter2D(collision, prefabOf);
        if (((IProjectile)this).ProjectileHit(gameObject, collision.GameObject()))
        {
            StartCoroutine(DestroyAfterDuration(prefabOf.GetComponent<Ability>().afterLifetime));
            direction = Vector2.zero;
        }
    }
}
