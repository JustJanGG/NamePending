using UnityEngine;

public interface IAoE
{
    AoEStats aoeStats { get; set; }

    public void InitiateAoE()
    {
        // no reduced stats
    }

    public void DefaultAoEBehaviour(GameObject gameObject)
    {
        gameObject.GetComponent<CircleCollider2D>().radius = aoeStats.areaSize;
    }
}
