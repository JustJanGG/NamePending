using System.Collections.Generic;
using UnityEngine;

public class PierceCircuit : RedCircuit
{
    public PierceCircuit()
    {
        id = 2;
        circuitName = "Pierce";
        circuitDescription = "Increases Pierce count by 2.";
        circuitType = CircuitType.Red;
        tags = new List<Tag>();
        tags.Add(Tag.Projectile);
    }

    public override void ApplyRedCircuit(Ability ability)
    {
        if(ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().pierce += 2;
        }
    }

    public override void RemoveRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().pierce -= 2;
        }
    }
}
