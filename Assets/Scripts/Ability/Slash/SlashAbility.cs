using UnityEngine;

public class SlashAbility : Ability
{
    public override void Hit(GameObject enemy)
    {
        Hit hit = new(enemy, this, GetBlueCircuits(), DealDamage());
    }

    public override void Activate()
    {
        audioSource.PlayOneShot(audioClips[0]);
        GameObject slash = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity, player.gameObject.transform);
        SlashPrefab slashPrefab = slash.GetComponent<SlashPrefab>();
        slashPrefab.prefabOf = this.gameObject;
        slashPrefab.meleeStats = this.gameObject.GetComponent<MeleeStats>();
        slashPrefab.audioClips = this.audioClips;
        audioSource.PlayOneShot(audioClips[0]);

        SetCooldown();
    }
}
