using UnityEngine;

public class FireballPrefab : Projectile
{
    public FireballAbility fireballAbility;

    void Start()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f); 
    }
}
