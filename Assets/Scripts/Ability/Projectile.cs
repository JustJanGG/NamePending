using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector2 direction;

    public float speed;
    public ProjecileStats projecileStats;


    void Start()
    {
        
    }

    public void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
    }
}
