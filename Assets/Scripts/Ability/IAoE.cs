using UnityEngine;

public interface IAoE
{
    AoEStats aoeStats { get; set; }

    public void InitiateAoe()
    {
        // no reduced stats
    }

    public void DefaultAoEBehaviour(GameObject gameObject)
    {
        
    }
}
