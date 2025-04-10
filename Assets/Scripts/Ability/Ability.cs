using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using System;
public abstract class Ability : MonoBehaviour
{
    public GameObject abilityPrefab;
    public List<GameObject> circuits;

    protected GameObject player;
    protected int id;
    protected string abilityName;
    protected string abilityDescription;
    protected float procCoefficiant;
    protected float cooldown;
    protected List<Tag> tags;
    protected float physicalDamage;
    protected float fireDamage;

    [Header("Projectile Stats")]
    public float projectileSpeed;
    public int projectileCount;

    [Header("Area of Effect Stats")]
    public float areaOfEffect;

    public abstract List<BlueCircuit> GetBlueCircuits();
    public void ProcBlueCircuit(Hit hit, BlueCircuit blueCircuit, GameObject enemy)
    {

    }
    public abstract void UseAbility(InputAction.CallbackContext ctx);
}
