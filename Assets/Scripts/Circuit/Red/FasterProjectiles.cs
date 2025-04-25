using System.Collections.Generic;
using UnityEngine;

public class FasterProjectiles : RedCircuit
{
    public FasterProjectiles()
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
        ((ProjecileStats)ability.stats).projectileSpeed *= 1.5f;
        //ability.projectileSpeed *= 1.5f;
    }
    public override void RemoveRedCircuit(Ability ability)
    {
        ((ProjecileStats)ability.stats).projectileSpeed /= 1.5f;
        //ability.projectileSpeed /= 1.5f;
    }
}
