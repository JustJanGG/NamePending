using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class FireballAbility : Ability
{

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circuits = new List<GameObject>();
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
    public void Hit(GameObject enemy)
    {
        Debug.Log("Fireball Hit");
        Hit hit = new(enemy, this);
    }
    public override void UseAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject fireball = Instantiate(abilityPrefab, transform.position, Quaternion.identity);

            FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
            fireballPrefab.speed = projectileSpeed;
        }
    }

    public void ApplyRedCircuits()
    {
        Debug.Log("Applying red circuits to fireball");
        foreach (var circuit in circuits)
        {
            if (circuit.GetComponent<ICircuit>().circuitType == CircuitType.Red)
            {
                circuit.GetComponent<RedCircuit>().ApplyRedCircuit(this);
            }
        }
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
