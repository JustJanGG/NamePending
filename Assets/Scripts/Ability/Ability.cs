using System.Collections.Generic;
using UnityEngine;

using System;
public abstract class Ability : MonoBehaviour
{
    public GameObject abilityPrefab;
    public List<GameObject> circuits;

    protected GameObject player;
    //protected int id;
    protected Dictionary<DamageType, float> damage;

    public List<Tag> tags;

    [Header("Damage")]
    public float physicalOfBase;
    public float fireOfBase;
    public float coldOfBase;
    public float lightningOfBase;

    [Header("General Stats")]
    public float attackSpeed; // duration of ability
    public float castTime; // time to cast a ability
    public float cooldown; // cooldown until ability can be pressed again
    public float afterLifetime; // time the prefab lingers after effect
    public float procCoefficient;

    [Header("Logic")]
    private float cooldownTimer;

    [Header("Audio")]
    public AudioClip castSound;
    public AudioClip hitSound;

    private void Awake()
    {
        cooldownTimer = 0;
        circuits = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public List<BlueCircuit> GetBlueCircuits()
    {
        List<BlueCircuit> blueCircuits = new List<BlueCircuit>();
        foreach (GameObject circuit in circuits)
        {
            if (circuit.GetComponent<ICircuit>().circuitType == CircuitType.Blue)
            {
                blueCircuits.Add(circuit.GetComponent<BlueCircuit>());
            }
        }
        return blueCircuits;
    }

    public void ProcBlueCircuit(BlueCircuit blueCircuit, GameObject enemy, List<BlueCircuit> reducedList, Dictionary<DamageType, float> damage)
    {
        if (enemy != null)
        {
            blueCircuit.Activate(enemy, reducedList, damage);
        }
    }

    public void ApplyCircuit(GameObject circuit)
    {
        if (circuit.tag == "Circuit")
        {
            circuits.Add(circuit);
            switch (circuit.GetComponent<ICircuit>().circuitType)
            {
                case CircuitType.Red:
                    circuit.GetComponent<RedCircuit>().ApplyRedCircuit(this);
                    foreach (var item in circuits)
                    {
                        if (item.GetComponent<ICircuit>().circuitType == CircuitType.Blue)
                        {
                            item.GetComponent<BlueCircuit>().ApplyCircuit(circuit);
                        }
                    }
                    break;
                case CircuitType.Blue:
                    foreach (var item in circuits)
                    {
                        if (item.GetComponent<ICircuit>().circuitType == CircuitType.Red)
                        {
                            circuit.GetComponent<BlueCircuit>().ApplyCircuit(item);
                        }
                    }
                    break;
                case CircuitType.Green:
                    // TODO: Implement Green Circuit logic
                    break;
                default:
                    Debug.LogError("Invalid circuit type");
                    break;
            }

        }
    }

    public void RemoveCircuit(GameObject circuit)
    {
        if (circuit.tag == "Circuit" && circuits.Contains(circuit))
        {
            circuits.Remove(circuit);
            switch (circuit.GetComponent<ICircuit>().circuitType)
            {
                case CircuitType.Red:
                    circuit.GetComponent<RedCircuit>().RemoveRedCircuit(this);
                    foreach (var item in circuits)
                    {
                        if (item.GetComponent<ICircuit>().circuitType == CircuitType.Blue)
                        {
                            item.GetComponent<BlueCircuit>().RemoveCircuit(circuit);
                        }
                    }
                    break;
                case CircuitType.Blue:
                    foreach (var item in circuits)
                    {
                        if (item.GetComponent<ICircuit>().circuitType == CircuitType.Red)
                        {
                            circuit.GetComponent<BlueCircuit>().RemoveCircuit(item);
                        }
                    }
                    break;
                case CircuitType.Green:
                    // TODO: Implement Green Circuit logic
                    break;
                default:
                    Debug.LogError("Invalid circuit type");
                    break;
            }

        }
    }

    public bool CheckCooldown()
    {
        return cooldownTimer <= 0f;
    }
        
    public void SetCooldown()
    {
        cooldownTimer = cooldown;
    }

    public abstract void Activate();

    public Dictionary<DamageType, float> DealDamage()
    {
        float baseDamage = player.GetComponent<PlayerStats>().baseDamage;
        Dictionary<DamageType, float> damage = new Dictionary<DamageType, float>();
        damage.Add(DamageType.Physical, physicalOfBase * baseDamage);
        damage.Add(DamageType.Fire, fireOfBase * baseDamage);
        damage.Add(DamageType.Cold, coldOfBase * baseDamage);
        damage.Add(DamageType.Lightning, lightningOfBase * baseDamage);
        return damage;
    }

    public abstract void Hit(GameObject enemy);
}
