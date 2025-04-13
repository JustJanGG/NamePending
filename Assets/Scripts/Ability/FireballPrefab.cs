using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    private Vector2 direction;
    private FireballAbility fireballAbility;

    [Header("Fireball properties")]
    public float speed = 5.0f;

    void Start()
    {
        fireballAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<FireballAbility>();
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f); 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            fireballAbility.Hit(collision.gameObject); 
        }
    }
}
