using System.Collections.Generic;
using UnityEngine;

public class FasterProjectilesCircuit : RedCircuit
{
    public float speedMultiplier;
    public FasterProjectilesCircuit()
    {
        id = 1;
        circuitName = "Faster Projectiles";
        circuitDescription = "Increases the speed of projectiles by 50%.";
        circuitType = CircuitType.Red;
        tags = new List<Tag>();
        tags.Add(Tag.Projectile);
    }

    public override void ApplyRedCircuit(Ability ability)
    {
        if(ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().projectileSpeed *= speedMultiplier;
        }
    }

    public override void RemoveRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().projectileSpeed /= speedMultiplier;
        }
    }
}
