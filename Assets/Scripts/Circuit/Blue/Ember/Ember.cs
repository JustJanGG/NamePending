using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ember : BlueCircuit
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circuitName = "Ember";
        circuitType = CircuitType.Blue;
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        if (CheckCooldown())
        {
            GameObject ember = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            EmberPrefab emberPrefab = ember.GetComponent<EmberPrefab>();

            ((IBlueCircuitPrefab)emberPrefab).PassDamage(damage);
            emberPrefab.prefabOf = this.gameObject;
            ((IBlueCircuitPrefab)emberPrefab).PassList(blueCircuits);
            emberPrefab.target = enemy.transform;
            emberPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();
            SetCooldown();
        }
    }

}
