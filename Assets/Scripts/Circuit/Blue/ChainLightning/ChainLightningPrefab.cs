using System.Collections.Generic;
using UnityEngine;

public class ChainLightningPrefab : AbilityPrefab, IBlueCircuitPrefab
{
    public List<BlueCircuit> reducedList { get; set; }
    public Dictionary<DamageType, float> damage { get; set; }
}
