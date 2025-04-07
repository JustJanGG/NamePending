using UnityEngine;
using System.Collections.Generic;


public class Fireball : Ability
{

    public Fireball()
    {
        tags = new List<Tag>();
        tags.Add(Tag.AoE);
        tags.Add(Tag.Projectile);
        tags.Add(Tag.Ranged);
        abilityDescription = "A fireball that explodes on impact, dealing damage to all enemies in the area.";
        abilityName = "Fireball";
        procCoefficiant = 1f;
        cooldown = 0f;
    }
}
