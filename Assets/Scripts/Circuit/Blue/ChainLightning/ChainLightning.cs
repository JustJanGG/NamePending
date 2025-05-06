using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : BlueCircuit
{
    public Ability socketedAbility { get; set; }
    public List<RedCircuit> redCircuits { get; set; }
    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        GameObject chainLightning = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        ChainLightningPrefab chainLightningPrefab = chainLightning.GetComponent<ChainLightningPrefab>();

        ((IBlueCircuitPrefab)chainLightningPrefab).PassDamage(damage);
        chainLightningPrefab.prefabOf = this.gameObject;
        ((IBlueCircuitPrefab)chainLightningPrefab).PassList(blueCircuits);
    }
}
