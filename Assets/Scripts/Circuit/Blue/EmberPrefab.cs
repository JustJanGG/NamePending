using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmberPrefab : MonoBehaviour
{
    private Vector2 direction;
    private Ember emberAbility;
    private List<BlueCircuit> reducedList;

    public Transform target;
    [Header("Fireball properties")]
    public float speed;

    void Start()
    {
        emberAbility = this.transform.parent.GetComponent<Ember>();
        direction = target.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f);
    }
    public void PassList(List<BlueCircuit> reducedList)
    {
        this.reducedList = reducedList;
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            emberAbility.Hit(collision.gameObject, reducedList);
        }
    }
}
