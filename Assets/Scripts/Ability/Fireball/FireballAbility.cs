using UnityEngine;
using System.Collections.Generic;


public class FireballAbility : Ability
{
    //public ProjecileStats projecileStats;
    public void Start()
    {

        tags = new List<Tag>();
        tags.Add(Tag.AoE);
        tags.Add(Tag.Projectile);
        tags.Add(Tag.Ranged);
        abilityDescription = "A Projectile that travels in a straight line and deals damage on impact";
        abilityName = "Fireball";
    }

    public override void Hit(GameObject enemy)
    {
       Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }
    public override void Activate()
    {
        GameObject fireball = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);
        FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
        fireballPrefab.prefabOf = this.gameObject;
    }

}
