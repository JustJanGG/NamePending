using System.Collections.Generic;
using UnityEngine;

public interface IChaining
{
    public ChainingStats chainingStats { get; set; }

    public GameObject FindClosestEnemy(float range, GameObject lastEnemyHit, Vector3 lastEnemyTrans, List<GameObject> alreadyHit)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            return null;
        }
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
