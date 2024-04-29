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
    public List<UpgradeResourceData> upgradeVariables;
}

[Serializable]
public class UpgradeResourceData {
    public float upgradeCostBase;
    public float upgradeCostMultiplier;
    public float upgradeInfluenceBase;
    public float upgradeInfluenceMultiplier;
    public ResourceType upgradeResourceType;
}
