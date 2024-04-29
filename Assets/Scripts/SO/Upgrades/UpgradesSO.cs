using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesData", menuName = "ScriptableObjects/UpgradeSO")]
public class UpgradesSO : ScriptableObject
{
    //>> Lvling rules
    public bool lvlAbove;
    //->>

    public float upgradeBase;
    public List<UpgradeVariables> upgradeVariables;

    public List<UpgradeInfluenceData> upgradeTarget;
}

[Serializable]
public class UpgradeVariables {
    public float costBase;
    public float costMultiplier;
    public ResourceType resourceCostType;   
}

[Serializable]

public class UpgradeInfluenceData {
    public ResourceType influencedResourceType;
    public UpgradedVariable influencedVariableType;
    public float influenceBase;
    public float influenceMultiplier;
}
