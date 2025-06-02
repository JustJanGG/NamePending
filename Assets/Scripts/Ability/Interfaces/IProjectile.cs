using UnityEngine;

public interface IProjectile
{
    Vector2 direction { get; set; }
    ProjecileStats projecileStats { get; set; }
    int pierceCount { get; set; }

    public void InitiateProjectile()
    {
        pierceCount = projecileStats.pierce;
    }

    public void DefaultProjectileMovement(GameObject gameObject)
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * projecileStats.projectileSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ProjectileHit(GameObject projectile)
    {
        if (pierceCount <= 0)
        {
            GameObject.Destroy(projectile);
        }
        else
        {
            pierceCount--;
        }
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
