using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenRockPrefab : AbilityPrefab, IProjectile, IAoE, IBlueCircuitPrefab
{
    [HideInInspector]
    public Transform enemy;
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ProjecileStats projecileStats { get; set; }
    public Vector2 direction { get; set; }
    public int pierceCount { get; set; }
    public AoEStats aoeStats { get; set; }

    private void Start()
    {
        ((IProjectile)this).InitiateProjectile();
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(-1, 1);
        direction = new Vector3(randomX, randomY, 0f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, prefabOf.GetComponent<Ability>().lifetime);
    }

    private void Update()
    {
        StartCoroutine(MoltenRockBehaviour());
    }

    private IEnumerator MoltenRockBehaviour()
    {
        // change to parabolic arc until second behaviour
        ((IProjectile)this).DefaultProjectileMovement(gameObject);
        yield return new WaitForSeconds(1f);
        // change to AoE behaviour
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        transform.Find("ExplosionSprite").gameObject.SetActive(true);
        ((IAoE)this).DefaultAoEBehaviour(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ((IBlueCircuitPrefab)this).DefaultBlueCircuitOnTriggerEnter2D(collision, prefabOf);
    }
}
