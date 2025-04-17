using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability")]
    public List<GameObject> abilities;

    private void Start()
    {
        abilities = new List<GameObject>();
        AddAbilitesFromChildren();
    }

    private void AddAbilitesFromChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject ability = child.GetComponentInChildren<Ability>().gameObject;
            if (ability.CompareTag("Ability"))
            {
                abilities.Add(ability);
            }
        }
    }

}
