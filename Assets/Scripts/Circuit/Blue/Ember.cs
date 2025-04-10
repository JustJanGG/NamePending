using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ember : Ability, IBlueCircuit
{
    public int id { get; set; }
    public string circuitName { get; set; }
    public string circuitDescription { get; set; }
    public CircuitType circuitType { get; set; }
    public float procCoefficient { get; set; }
    public float procChance { get; set; }
    public Ability socketedAbility { get; set; }
    public List<RedCircuit> redCircuits { get; set; }
    public void UseAbility()
    {
        GameObject fireball = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
        fireballPrefab.speed = projectileSpeed;
    }

    public override float[] DealDamage()
    {
        //TODO
        float[] damage = new float[4];
        damage[0] = physicalDamage;
        damage[1] = fireDamage;
        damage[2] = 0f; //cold
        damage[3] = 0f; //Lightning
        return damage;
    }
    public override void UseAbility(InputAction.CallbackContext ctx)
    {

    }
}
