using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class FireballAbility : Ability
{

    public void Start()
    {
        circuits = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        tags = new List<Tag>();
        tags.Add(Tag.AoE);
        tags.Add(Tag.Projectile);
        tags.Add(Tag.Ranged);
        abilityDescription = "A fireball that explodes on impact, dealing damage to all enemies in the area.";
        abilityName = "Fireball";
        procCoefficiant = 1f;
        cooldown = 0f;
        physicalDamage = 0f;
        fireDamage = 10f;
        projectileSpeed = 10f;
        areaOfEffect = 1f;
        projectileCount = 1;

    }

    public override void Hit(GameObject enemy)
    {
        //Debug.Log("Fireball Hit");
         
        //foreach (var circuit in circuits)
        //{
        //    Debug.Log(circuit.GetComponent<ICircuit>().circuitType);
        //}
        Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }
    public override void Activate()
    {
        GameObject fireball = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);

        FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
        fireballPrefab.speed = projectileSpeed;
    }


    public override float[] DealDamage()
    {
        float[] damage = new float[4];
        damage[0] = physicalDamage;
        damage[1] = fireDamage;
        damage[2] = 0f; //cold
        damage[3] = 0f; //Lightning
        return damage;
    }
}
