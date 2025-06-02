using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChainLightningPrefab : AbilityPrefab, IBlueCircuitPrefab, IChaining
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
    public ChainingStats chainingStats { get; set; }

    private List<GameObject> alreadyHit;
    public GameObject lastEnemyHit;
    private Vector3 lastEnemyTrans;


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
        lastEnemyTrans = new Vector3(lastEnemyHit.transform.position.x, lastEnemyHit.transform.position.y, 0f);
        GameObject closestEnemy;


        for (int i = 0; i < chainingStats.chainCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            closestEnemy = FindClosestEnemy(chainingStats.chainRange);
            if (closestEnemy == lastEnemyHit || closestEnemy == null)
            {
                break;
            }
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, closestEnemy.transform.position, chainingStats.chainRange);
            alreadyHit.Add(closestEnemy);
            prefabOf.GetComponent<BlueCircuit>().Hit(closestEnemy, reducedList, damage);
        }
        yield return null;
    }
    private GameObject FindClosestEnemy(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = lastEnemyHit;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(lastEnemyTrans, enemy.transform.position);
                if (distance < closestDistance
                    && distance <= range
                    && !alreadyHit.Contains(enemy))
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        if (closestEnemy == null)
        {
            return null;
        }

        lastEnemyTrans = new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.position.y, 0f);
        return closestEnemy;
        
    }
}
