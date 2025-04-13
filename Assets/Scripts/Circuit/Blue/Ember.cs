using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ember : BlueCircuit
{

    public float procCoefficient { get; set; }
    public float procChance { get; set; }
    public Ability socketedAbility { get; set; }
    public List<RedCircuit> redCircuits { get; set; }

    private void Start()
    {
        procCoefficient = 1;
        procChance = 1;
        circuitType = CircuitType.Blue;
        //Debug.Log(circuitType);
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, float[] damage)
    {
        GameObject fireball = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        EmberPrefab emberPrefab = fireball.GetComponent<EmberPrefab>();
        emberPrefab.PassList(blueCircuits);
        emberPrefab.speed = projectileSpeed;
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
    public override void Hit(GameObject enemy, List<BlueCircuit> reducedList)
    {
        Debug.Log("Ember Hit");
        Hit hit = new(enemy, this, reducedList, DealDamage());
    }
}
