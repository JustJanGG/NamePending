using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int health = 3;
    public int maxHealth = 3;
    public int healthRegen;

    [Header("Movement")]
    public float movementSpeed = 5.0f;
    public float dashRange = 10.0f;
    public float dashCooldown = 1.0f;

    [Header("DamageStats")]
    public float fireDamage = 5.0f;

    #region Getters
    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealthRegen()
    {
        return healthRegen;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetDashRange()
    {
        return dashRange;
    }

    public float GetDashCooldown()
    {
        return dashCooldown;
    }
    #endregion

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: give stats to Controller when changed with setter
    }

}
