using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector2 direction;
    public GameObject prefabOf;

    public bool isBlueCircuit;
    public ProjecileStats projecileStats;
    private void Awake()
    {

    }
    public void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * projecileStats.projectileSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            prefabOf.GetComponent<Ability>().Hit(collision.gameObject);
        }
    }
}
