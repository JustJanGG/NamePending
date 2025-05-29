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
        gameObject.transform.localScale = new Vector3(aoeStats.areaSize, aoeStats.areaSize, 1f);
    }
}
