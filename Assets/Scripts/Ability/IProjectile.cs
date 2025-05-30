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

}
