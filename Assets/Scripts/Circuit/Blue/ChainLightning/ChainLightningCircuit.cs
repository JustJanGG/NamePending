using System.Collections.Generic;
using UnityEngine;

public class ChainLightningCircuit : BlueCircuit
{
    public Ability socketedAbility { get; set; }
    public List<RedCircuit> redCircuits { get; set; }
    public int chainCount;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circuitName = "Chain Lighting";
        circuitType = CircuitType.Blue;
    }
    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        GameObject chainLightning = Instantiate(abilityPrefab, enemy.transform.position, Quaternion.identity);
        ChainLightningPrefab chainLightningPrefab = chainLightning.GetComponent<ChainLightningPrefab>();

        ((IBlueCircuitPrefab)chainLightningPrefab).PassDamage(damage);
        chainLightningPrefab.prefabOf = this.gameObject;
        ((IBlueCircuitPrefab)chainLightningPrefab).PassList(blueCircuits);
        ((IChaining)chainLightningPrefab).chainingStats = this.gameObject.GetComponent<ChainingStats>();
        chainLightningPrefab.lastEnemyHit = enemy;
    }
}
