using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class EmberCircuit : BlueCircuit
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
            float arcAngle = 45f;
            for (int i = 0; i < this.GetComponent<ProjecileStats>().projectileCount; i++)
            {
                GameObject ember = Instantiate(abilityPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                EmberPrefab emberPrefab = ember.GetComponent<EmberPrefab>();

                ((IBlueCircuitPrefab)emberPrefab).PassDamage(damage);
                emberPrefab.prefabOf = this.gameObject;
                ((IBlueCircuitPrefab)emberPrefab).PassList(blueCircuits);
                emberPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();

                Vector2[] directions = emberPrefab.GetComponent<IProjectile>().CalculateProjectileArc(GetComponent<ProjecileStats>().projectileCount, arcAngle, gameObject.transform.position, enemy.transform.position - emberPrefab.transform.position);
                emberPrefab.direction = directions[i];

                SetCooldown();
            }
        }
    }

}
