using System.Collections.Generic;
using UnityEngine;

using System;
public abstract class Ability : MonoBehaviour
{
    public GameObject prefab;
    protected int id;
    protected string abilityName;
    protected string abilityDescription;
    protected float procCoefficiant;
    protected float cooldown;
    protected List<Tag> tags;
    protected List<Circuit> circuits;
}
