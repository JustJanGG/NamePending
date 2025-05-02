using UnityEngine;

public interface IMelee
{
    Vector2 direction { get; set; }
    MeleeStats meleeStats { get; set; }

    public void InitiateMelee()
    {
       // set stats
    }

    public void DefaultMeleeBehaviour(GameObject gameObject)
    {
        // swing frontal
    }

}
