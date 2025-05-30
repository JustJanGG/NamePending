using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningPrefab : AbilityPrefab, IBlueCircuitPrefab
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public float chainRange = 1000f;
    //public GameObject initialEnemy;
    private List<GameObject> alreadyHit;
    public int chainCount;
    public GameObject lastEnemyHit;


    private void Start()
    {
        StartCoroutine(ChainLightningBehaviour());
        Destroy(gameObject, prefabOf.GetComponent<Ability>().lifetime);
    }

    private IEnumerator ChainLightningBehaviour()
    {
        prefabOf.GetComponent<BlueCircuit>().Hit(lastEnemyHit, reducedList, damage);
        alreadyHit = new List<GameObject>();
        alreadyHit.Add(lastEnemyHit);
        GameObject closestEnemy;

        for (int i = 0; i < chainCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            closestEnemy = FindClosestEnemy(lastEnemyHit.transform, chainRange);
            if (closestEnemy == lastEnemyHit)
            {
                break;
            }
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, closestEnemy.transform.position, chainRange);
            alreadyHit.Add(closestEnemy);
            prefabOf.GetComponent<BlueCircuit>().Hit(closestEnemy, reducedList, damage); 
        }
        yield return null;
    }
    private GameObject FindClosestEnemy(Transform startPoint, float range)
    {
        //Fix no enemies found
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = lastEnemyHit;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(lastEnemyHit.transform.position, enemy.transform.position);
            if (enemy != null
                && distance < closestDistance 
                && distance <= range
                && !alreadyHit.Contains(enemy))
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
