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

    [Header("Resistances")]
    [Range(0, 1)]
    public float physicalResistance;
    [Range(0, 1)]
    public float fireResistance;
    [Range(0, 1)]
    public float coldResistance;
    [Range(0, 1)]
    public float lightningResistance;
    

    private Dictionary<DamageType, float> resistances = new();

    private void Start()
    {
        resistances.Add(DamageType.Physical, physicalResistance);
        resistances.Add(DamageType.Fire, fireResistance);
        resistances.Add(DamageType.Cold, coldResistance);
        resistances.Add(DamageType.Lightning, lightningResistance);
    }
    public void TakeDamage(Dictionary<DamageType, float> damage)
    {
        foreach (var item in damage)
        {
            int damageTaken = (int)(damage[item.Key] * (1 - resistances[item.Key]));
            health -= damageTaken;
            if (damageTaken != 0)
            {
                GameManager.instance.GetComponent<DamagePopupManager>().ShowDamagePopup(damageTaken, item.Key, transform.position);
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
