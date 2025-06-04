using System.Collections.Generic;
using UnityEngine;

public class MultipleProjectilesCircuit : RedCircuit
{
    public MultipleProjectilesCircuit()
    {
        id = 3;
        circuitName = "Multiple Projectiles";
        circuitDescription = "Increases the amount of projectiles by 2.";
        circuitType = CircuitType.Red;
        tags = new List<Tag>();
        tags.Add(Tag.Projectile);
    }

    public override void ApplyRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().projectileCount += 2;
        }
    }

    public override void RemoveRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().projectileCount -= 2;
        }
    }

}
