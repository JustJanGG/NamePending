using UnityEngine;

public class FireballAbility : Ability
{
    public override void Hit(GameObject enemy)
    {   
        Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }

    public override void Activate()
    {
        GameObject fireball = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);
        FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
        fireballPrefab.prefabOf = this.gameObject;
        fireballPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();
        SetCooldown();
    }

}
