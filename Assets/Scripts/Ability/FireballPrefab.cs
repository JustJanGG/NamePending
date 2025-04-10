using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    private Vector2 direction;

    [Header("Fireball properties")]
    public float speed = 5.0f;
    public int damage = 1;
    public int projectileCount = 1;
    public float areaOfEffect = 1.0f;
    public float castDelay = 1f;

    void Start()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f); 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
