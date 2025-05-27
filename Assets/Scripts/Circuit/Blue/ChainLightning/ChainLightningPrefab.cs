using System.Collections.Generic;
using UnityEngine;

public class ChainLightningPrefab : AbilityPrefab, IBlueCircuitPrefab
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    private void Start()
    {
        
    }
    private GameObject FindClosestEnemy(Transform startPoint, float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = GameObject.FindGameObjectWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null
                && Vector3.Distance(closestEnemy.transform.position, startPoint.position) < closestDistance 
                && Vector3.Distance(closestEnemy.transform.position, startPoint.position) <= range)
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
