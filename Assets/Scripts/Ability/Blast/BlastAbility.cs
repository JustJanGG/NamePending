using UnityEngine;

public class BlastAbility : Ability
{
    public override void Hit(GameObject enemy)
    {
        Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }

    public override void Activate()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        GameObject blast = Instantiate(abilityPrefab, mouseWorldPos, Quaternion.identity);
        BlastPrefab blastPrefab = blast.GetComponent<BlastPrefab>();
        blastPrefab.prefabOf = this.gameObject;
        blastPrefab.aoeStats = this.gameObject.GetComponent<AoEStats>();
        SetCooldown();
    }
}
