using UnityEngine;

public class AbilityPrefab : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabOf;

    protected void DefaultOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<Ability>().Hit(collision.gameObject);
        }
    }
}
