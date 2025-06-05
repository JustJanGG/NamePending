using UnityEngine;

public class AbilityPrefab : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabOf;

    [Header("Audio")]
    public AudioSource audioSource;

    protected void DefaultOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<Ability>().Hit(collision.gameObject);
        }
    }
}
