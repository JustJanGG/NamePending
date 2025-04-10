using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    private int health = 3;
    private int maxHealth = 3;
    private int healthRegen;

    [Header("Movement")]
    private float movementSpeed = 5.0f;
    private float dashRange = 10.0f;
    private float dashCooldown = 1.0f;

    [Header("Damage")]
    private float fireDamage = 5.0f;

    #region Getter
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
    public float GetFireDamage()
    {
        return fireDamage;
    }
    #endregion

    #region Setter
    public void SetHealth(int value)
    {
        health = value;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }

    public void SetHealthRegen(int value)
    {
        healthRegen = value;
    }

    public void SetMovementSpeed(float value)
    {
        movementSpeed = value;
    }

    public void SetDashRange(float value)
    {
        dashRange = value;
    }

    public void SetDashCooldown(float value)
    {
        dashCooldown = value;
    }

    public void SetFireDamage(float value)
    {
        fireDamage = value;
    }

    #endregion

}
