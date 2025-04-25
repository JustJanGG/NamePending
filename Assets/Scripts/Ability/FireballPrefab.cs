using UnityEngine;

public class FireballPrefab : Projectile
{
    private FireballAbility fireballAbility;

    void Start()
    {
        fireballAbility = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FireballAbility>();
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            fireballAbility.Hit(collision.gameObject); 
        }
    }
}
