using UnityEngine;

public interface IProjectile
{
    Vector2 direction { get; set; }

    ProjecileStats projecileStats { get; set; }


    public void DefaultProjectileMovement(GameObject gameObject)
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * projecileStats.projectileSpeed * Time.deltaTime;
    }
}
