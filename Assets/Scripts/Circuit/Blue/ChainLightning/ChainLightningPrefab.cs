using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChainLightningPrefab : AbilityPrefab, IBlueCircuitPrefab, IChaining
{
    [HideInInspector]
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
        GetComponentInChildren<ParticleSystem>().Play();

        for (int i = 0; i < chainingStats.chainCount; i++)
        {
            yield return new WaitForSeconds(chainingStats.chainDelay);
            closestEnemy = ((IChaining)this).FindClosestEnemy(chainingStats.chainRange, lastEnemyHit, lastEnemyTrans, alreadyHit);
            if (closestEnemy == lastEnemyHit || closestEnemy == null)
            {
                break;
            }
            GetComponentInChildren<ParticleSystem>().Play();
            yield return StartCoroutine(MoveToPosition(closestEnemy.transform.position, chainingStats.chainRange * 2f));
            alreadyHit.Add(closestEnemy);
            prefabOf.GetComponent<BlueCircuit>().Hit(closestEnemy, reducedList, damage);
        }
        yield return null;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float speed)
    {
        while (Vector2.Distance(gameObject.transform.position, targetPosition) > 0.1f)
        {
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.position = targetPosition;
    }
}
