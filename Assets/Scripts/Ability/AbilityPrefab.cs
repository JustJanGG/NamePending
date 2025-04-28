using UnityEngine;

public class AbilityPrefab : MonoBehaviour
{
    public GameObject prefabOf;
    public bool isBlueCircuit;

    protected void DefaultOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<Ability>().Hit(collision.gameObject);
        }
    }
}
