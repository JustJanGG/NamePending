using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public int maxHealth;

    [Header("Movement")]
    public float movementSpeed;
    public float aggroRange;

    [Header("Damage")]
    public float damage;

    public float[] resistances;

    private void Start()
    {
        resistances = new float[4];
    }
    public void TakeDamage(float[] damage)
    {
        for (int i = 0; i < damage.Length; i++)
        {
            health = -(int)(damage[i]* (1 - resistances[i]));
            if (health <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
