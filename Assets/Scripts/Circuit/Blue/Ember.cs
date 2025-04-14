using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ember : BlueCircuit
{

    public Ability socketedAbility { get; set; }
    public List<RedCircuit> redCircuits { get; set; }

    private void Start()
    {
        circuitName = "Ember";
        circuitType = CircuitType.Blue;
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, float[] damage)
    {
        GameObject ember = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        EmberPrefab emberPrefab = ember.GetComponent<EmberPrefab>();
        emberPrefab.PassList(blueCircuits);
        emberPrefab.target = enemy.transform;
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
        //Debug.Log("Ember Hit");
        Hit hit = new(enemy, this, reducedList, DealDamage());
    }
}
