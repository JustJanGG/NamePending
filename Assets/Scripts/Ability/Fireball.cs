using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class Fireball : Ability
{
    public float speedTest = 5.0f;
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
    public override void UseAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject fireball = Instantiate(abilityPrefab, transform.position, Quaternion.identity);

            FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
            fireballPrefab.speed = speedTest;
        }
    }
}
