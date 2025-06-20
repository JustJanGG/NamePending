using System.Collections;
using UnityEngine;

public class AbilityPrefab : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabOf;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audioClips; //[0] Cast/Activation Sound, [1] flight Sound, [2] Hit Sound

    protected void DefaultOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<Ability>().Hit(collision.gameObject);
        }
    }

    protected IEnumerator DestroyAfterDuration(float afterLifeTime)
    {
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(afterLifeTime);
        Destroy(this.gameObject);
    }
}
