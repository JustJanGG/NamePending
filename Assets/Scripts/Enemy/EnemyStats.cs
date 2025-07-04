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
    public float physicalDamage;
    public float fireDamage;
    public float coldDamage;
    public float lightningDamage;

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

    public Dictionary<DamageType, float> DealDamage()
    {
        Dictionary<DamageType, float> damage = new Dictionary<DamageType, float>();
        damage.Add(DamageType.Physical, physicalDamage);
        damage.Add(DamageType.Fire, fireDamage);
        damage.Add(DamageType.Cold, coldDamage);
        damage.Add(DamageType.Lightning, lightningDamage);
        return damage;
    }

    private void Die()
    {
        GameManager.instance.enemyList.Remove(gameObject);
        Destroy(gameObject);
    }
}
