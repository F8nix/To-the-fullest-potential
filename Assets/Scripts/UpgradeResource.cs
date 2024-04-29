using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeResource
{
    public float currentInfluence = 0;
    public float currentCost = 0;

    public UpgradesSO data;
    public UpgradeResourceData variables;
    public UpgradeResource (UpgradeResourceData variables, int lvl, UpgradesSO data) {
        this.data = data;
        this.variables = variables;
        GenerateCostOnLevel(lvl);
        GenerateInfluenceOnLevel(lvl);
    }
    
    public UpgradesSO upgradesData;

    public float GenerateCostOnLevel(int level) {
        return Mathf.Ceil((1/upgradesData.upgradeBase * variables.upgradeCostBase) * Mathf.Pow(variables.upgradeCostMultiplier, level-1));
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public float GenerateInfluenceOnLevel(int level) {
        return Mathf.Ceil((upgradesData.upgradeBase * variables.upgradeInfluenceBase) * Mathf.Pow(variables.upgradeInfluenceMultiplier, level-1));
    }

    public float GetResourceCurrentValue(ResourceType type) {
        return ResourceManager.Instance.resourcesConversion[type].CurrentValue;
    }
}
