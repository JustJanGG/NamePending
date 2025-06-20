using System.Collections.Generic;
using UnityEngine;

public class WaterBoltCircuit : BlueCircuit
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circuitName = "Water Bolt";
        circuitType = CircuitType.Blue;
    }

    public override void Activate(GameObject enemy, List<BlueCircuit> blueCircuits, Dictionary<DamageType, float> damage)
    {
        if (CheckCooldown())
        {
            Vector3 enemyPosition = enemy.transform.position;
            float arcAngle = 45f;
            for (int i = 0; i < this.GetComponent<ProjecileStats>().projectileCount; i++)
            {
                GameObject waterBolt = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                WaterBoltPrefab waterBoltPrefab = waterBolt.GetComponent<WaterBoltPrefab>();

                ((IBlueCircuitPrefab)waterBoltPrefab).PassDamage(damage);
                waterBoltPrefab.prefabOf = this.gameObject;
                waterBoltPrefab.audioClips = this.audioClips;
                audioSource.PlayOneShot(audioClips[0]);
                ((IBlueCircuitPrefab)waterBoltPrefab).PassList(blueCircuits);
                waterBoltPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();

                Vector2[] directions = waterBoltPrefab.GetComponent<IProjectile>().CalculateProjectileArc(GetComponent<ProjecileStats>().projectileCount, arcAngle, gameObject.transform.position, enemyPosition - waterBoltPrefab.transform.position);
                waterBoltPrefab.direction = directions[i];

                SetCooldown();
            }
        }
    }

}
