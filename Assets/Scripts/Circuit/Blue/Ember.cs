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
        player = GameObject.FindGameObjectWithTag("Player");
        circuitName = "Ember";
        circuitType = CircuitType.Blue;
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        GameObject ember = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        EmberPrefab emberPrefab = ember.GetComponent<EmberPrefab>();
        emberPrefab.PassList(blueCircuits);
        emberPrefab.target = enemy.transform;
        emberPrefab.speed = projectileSpeed;
    }

}
