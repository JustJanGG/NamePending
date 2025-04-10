using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    [Header("Stats")]
    private int health = 5;
    private int maxHealth= 5;
    public float movementSpeed = 2.0f;
    public float aggroRange = 5.0f;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
