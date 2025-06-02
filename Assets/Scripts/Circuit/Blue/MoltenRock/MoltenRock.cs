using System.Collections.Generic;
using UnityEngine;

public class MoltenRock : BlueCircuit
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circuitName = "MoltenRock";
        circuitType = CircuitType.Blue;
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        if (CheckCooldown())
        {
            for (int i = 0; i < this.GetComponent<ProjecileStats>().projectileCount; i++)
            {
                GameObject moltenRock = Instantiate(abilityPrefab, enemy.transform.position, Quaternion.identity);
                MoltenRockPrefab moltenRockPrefab = moltenRock.GetComponent<MoltenRockPrefab>();

                ((IBlueCircuitPrefab)moltenRockPrefab).PassDamage(damage);
                moltenRockPrefab.prefabOf = this.gameObject;
                ((IBlueCircuitPrefab)moltenRockPrefab).PassList(blueCircuits);
                moltenRockPrefab.enemy = enemy.transform;
                moltenRockPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();
                moltenRockPrefab.aoeStats = this.gameObject.GetComponent<AoEStats>();
                SetCooldown();
            }
        }
    }

}
