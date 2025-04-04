using System.Collections.Generic;
using UnityEngine;

using System;
public abstract class Ability : MonoBehaviour
{
    private int id;
    private string abilityName;
    private string abilityDescription;
    private float procCoefficiant;
    private float cooldown;
    private List<Tag> tags;
    private List<Circuit> circuits;
}
