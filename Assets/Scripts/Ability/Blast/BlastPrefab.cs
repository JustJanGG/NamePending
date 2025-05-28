using UnityEngine;

public class BlastPrefab : AbilityPrefab, IAoE
{
    public AoEStats aoeStats { get; set; }

    void Start()
    {
        ((IAoE)this).InitiateAoe();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DefaultOnTriggerEnter2D(collision);
    }
}
