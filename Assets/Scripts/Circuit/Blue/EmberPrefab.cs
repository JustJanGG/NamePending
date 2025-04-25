using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmberPrefab : Projectile
{
    private List<BlueCircuit> reducedList;
    private Dictionary<DamageType, float> damage;
    public Ember emberAbility;

    public Transform target;

    void Start()
    {
        direction = target.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 3f);
    }
    public void PassList(List<BlueCircuit> reducedList)
    {
        this.reducedList = reducedList;
    }
    public void PassDamage(Dictionary<DamageType, float> damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            emberAbility.Hit(collision.gameObject, reducedList, damage);
        }
    }
}
