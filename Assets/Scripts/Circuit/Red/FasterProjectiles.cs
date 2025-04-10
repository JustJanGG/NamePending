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
        ability.projectileSpeed *= 1.5f;
    }
}
