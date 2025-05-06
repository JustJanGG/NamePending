using UnityEngine;

public class FireballAbility : Ability
{
    public void Start()
    {
        abilityDescription = "A Projectile that travels in a straight line and deals damage on impact";
        abilityName = "Fireball";
    }

    public override void Hit(GameObject enemy)
    {
       Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }
    public override void Activate()
    {
        int projectileCount = gameObject.GetComponent<ProjecileStats>().projectileCount;
        //float spreadAngle = 45;

        //for (int i = 0; i < projectileCount; i++)
        //{
            
        //}
        GameObject fireball = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);
        FireballPrefab fireballPrefab = fireball.GetComponent<FireballPrefab>();
        fireballPrefab.direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        fireballPrefab.prefabOf = this.gameObject;
        fireballPrefab.projecileStats = this.gameObject.GetComponent<ProjecileStats>();
    }

}