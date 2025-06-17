using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    Vector2 direction { get; set; }
    ProjecileStats projecileStats { get; set; }
    int pierceCount { get; set; }
    int chainCount { get; set; }
    List<GameObject> alreadyHitEnemies { get; set; }
    GameObject projectile { get; set; }

    public void InitiateProjectile(GameObject projectile)
    {
        this.projectile = projectile;
        alreadyHitEnemies = new List<GameObject>();
        pierceCount = projecileStats.pierce;
        chainCount = projecileStats.chainCount;
    }

    public void DefaultProjectileMovement(GameObject gameObject)
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * projecileStats.projectileSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public bool ProjectileHit(GameObject projectile, GameObject hit)
    {
        alreadyHitEnemies.Add(hit);
        if (pierceCount <= 0)
        {
            if (chainCount <= 0)
            {
                return true;
            }
            else
            {
                direction = FindClosestsEnemyDirection(hit);
                chainCount--;
            }
        }
        else
        {
            pierceCount--;
        }
        return false;
    }

    private Vector2 FindClosestsEnemyDirection(GameObject excludedEnemy)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            // No enemies found, return the current direction
            return direction;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector2 currentPosition = (Vector2)projectile.transform.position;

        foreach (GameObject enemy in enemies)
        {
            // Skip the excluded enemy and enemies already hit
            if (enemy == excludedEnemy || alreadyHitEnemies.Contains(enemy))
            {
                continue;
            }

            float distance = Vector2.Distance(currentPosition, (Vector2)enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // Add the closest enemy to the list of already hit enemies
            alreadyHitEnemies.Add(closestEnemy);

            // Calculate the direction toward the closest enemy
            Vector2 directionToEnemy = ((Vector2)closestEnemy.transform.position - currentPosition).normalized;
            return directionToEnemy;
        }

        // Fallback in case no valid enemy is found
        return direction;
    }
    public Vector2[] CalculateProjectileArc(int projectileCount, float arcAngle, Vector2 origin, Vector2 baseDirection)
    {
        Vector2[] directions = new Vector2[projectileCount];

        if (projectileCount == 1)
        {
            directions[0] = baseDirection;
        }
        else
        {
            float angleStep = arcAngle / (projectileCount - 1);
            float startAngle = -arcAngle / 2;
            for (int i = 0; i < projectileCount; i++)
            {
                float angle = startAngle + (angleStep * i);
                float radian = angle * Mathf.Deg2Rad;

                directions[i] = new Vector2(
                    baseDirection.x * Mathf.Cos(radian) - baseDirection.y * Mathf.Sin(radian),
                    baseDirection.x * Mathf.Sin(radian) + baseDirection.y * Mathf.Cos(radian)
                ).normalized;
            }
        }
        return directions;
    }

}
