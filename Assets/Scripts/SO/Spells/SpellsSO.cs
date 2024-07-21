using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellsData", menuName = "ScriptableObjects/SpellSO")]
public class SpellsSO : ScriptableObject
{
    //>> Lvling rules
    public bool lvlAbove;
    //->>

    public float spellBase;
    public List<SpellVariables> spellVariables;

    public List<SpellInfluenceData> spellTarget;
}

[Serializable]
public class SpellVariables {
    public float costBase;
    public float costMultiplier;
    public ResourceType resourceCostType;   
}

[Serializable]

public class SpellInfluenceData {
    public ResourceType influencedResourceType;
    public CharmedVariable influencedVariableType;
    public float influenceBase;
    public float influenceMultiplier;
}
