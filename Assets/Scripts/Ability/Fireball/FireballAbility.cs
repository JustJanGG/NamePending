using UnityEngine;

public class FireballAbility : Ability
{
    public override void Hit(GameObject enemy)
    {   
        Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }

    public override void Activate()
    {
        float arcAngle = 30f;
        for (int i = 0; i < this.GetComponent<ProjecileStats>().projectileCount; i++)
        {
            GameObject fireball = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);
            FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
            fireballPrefab.prefabOf = this.gameObject;
            fireballPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();

            fireballPrefab.castSound = this.castSound;
            fireballPrefab.hitSound = this.hitSound;

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 projectileStartPostion = gameObject.transform.position;
            Vector2 baseDirection = (cursorPosition - projectileStartPostion).normalized;

            Vector2[] directions = fireballPrefab.GetComponent<IProjectile>().CalculateProjectileArc(GetComponent<ProjecileStats>().projectileCount, arcAngle, gameObject.transform.position, baseDirection);
            fireballPrefab.direction = directions[i];

            SetCooldown();
        }
    }

}
