using System.Collections.Generic;
using UnityEngine;

public class ChainCircuit : RedCircuit
{
    public ChainCircuit()
    {
        id = 22;
        circuitName = "Chain";
        circuitDescription = "Increases Chain count by 2.";
        circuitType = CircuitType.Red;
        tags = new List<Tag>();
        tags.Add(Tag.Projectile);
        tags.Add(Tag.Chaining);
    }

    public override void ApplyRedCircuit(Ability ability)
    {
        if(ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().chainCount += 2;
        }
        else if (ability.tags.Contains(Tag.Chaining))
        {
            ability.GetComponent<ChainingStats>().chainCount += 2;
        }
    }

    public override void RemoveRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.Projectile))
        {
            ability.GetComponent<ProjecileStats>().chainCount -= 2;
        }
        else if (ability.tags.Contains(Tag.Chaining))
        {
            ability.GetComponent<ChainingStats>().chainCount -= 2;
        }
    }

}
