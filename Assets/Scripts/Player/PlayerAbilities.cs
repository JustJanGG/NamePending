using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability")]
    public List<GameObject> abilities;

    private void Awake()
    {
        abilities = new List<GameObject>();
        AddAbilitesFromChildren();
    }

    private void AddAbilitesFromChildren()
    {
        foreach (Transform child in transform)
        {
            Ability abilityComponent = child.GetComponentInChildren<Ability>();
            if (abilityComponent != null)
            {
                GameObject ability = abilityComponent.gameObject;
                if (ability.CompareTag("Ability"))
                {
                    abilities.Add(ability);
                }
            }
        }
    }

}
