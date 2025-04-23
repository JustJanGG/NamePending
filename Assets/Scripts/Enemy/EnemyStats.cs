using System.Collections.Generic;
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


    private Dictionary<DamageType, float> resistances = new Dictionary<DamageType, float>
    {
        { DamageType.Physical, 0f },
        { DamageType.Fire, 0f },
        { DamageType.Cold, 0f },
        { DamageType.Lightning, 0f }
    };

    private void Start()
    {

    }
    public void TakeDamage(Dictionary<DamageType, float> damage)
    {
        foreach (var item in damage)
        {
            health -= (int)(damage[item.Key] * (1 - resistances[item.Key]));
            if ((int)(damage[item.Key] * (1 - resistances[item.Key])) != 0)
            {
                GameManager.instance.GetComponent<DamagePopupManager>().ShowDamagePopup((int)(damage[item.Key] * (1 - resistances[item.Key])), transform.position);
            }
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
