using UnityEngine;

public class FireballProto : MonoBehaviour
{
    private Vector2 direction;

    [Header("Locations")]
    private Transform firePoint;
    private Transform target;
    
    [Header("Fireball properties")]
    public float speed = 5.0f;
    public int damage = 1;
    public int projectileCount = 1;
    public float areaOfEffect = 1.0f;
    public float castDelay = 1f; 

    private void Awake()
    {
        target = new GameObject().transform;
        firePoint = gameObject.transform;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.position = mousePos;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = (target.position - firePoint.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
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
