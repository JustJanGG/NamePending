using System.Collections.Generic;
using UnityEngine;

public class IncreasedAoECircuit : RedCircuit
{
    public float areaSizeMultiplier;
    public IncreasedAoECircuit()
    {
        id = 4;
        circuitName = "Increased AoE";
        circuitDescription = "Increases the size of AoE abilityies by 50%.";
        circuitType = CircuitType.Red;
        tags = new List<Tag>();
        tags.Add(Tag.AoE);
    }
    public override void ApplyRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.AoE))
        {
            ability.GetComponent<AoEStats>().areaSize *= areaSizeMultiplier;
        }
    }

    public override void RemoveRedCircuit(Ability ability)
    {
        if (ability.tags.Contains(Tag.AoE))
        {
            ability.GetComponent<AoEStats>().areaSize /= areaSizeMultiplier;
        }
    }
}
