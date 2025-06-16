using UnityEngine;

public class BlastPrefab : AbilityPrefab, IAoE
{
    public AoEStats aoeStats { get; set; }

    void Start()
    {
        ((IAoE)this).InitiateAoE();
        Destroy(gameObject, prefabOf.GetComponent<Ability>().afterLifetime);
    }

    void Update()
    {
        ((IAoE)this).DefaultAoEBehaviour(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DefaultOnTriggerEnter2D(collision);
    }
}
