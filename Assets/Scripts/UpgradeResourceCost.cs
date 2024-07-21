using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeResourceCost
{
    
    public float currentCost = 0;

    

    public UpgradesSO data;
    public UpgradeVariables variables;


    public UpgradeResourceCost (UpgradeVariables variables, int lvl, UpgradesSO data) {
        this.data = data;
        this.variables = variables;
        currentCost = GenerateCostOnLevel(lvl);
    }

    public float GenerateCostOnLevel(int level) {
        return Mathf.Ceil((1/data.upgradeBase * variables.costBase) * Mathf.Pow(variables.costMultiplier, level+1));
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public void UpdateOnLevelUp(int lvl) {
        
        Debug.Log(currentCost + "Cost");
        
        ResourceManager.Instance.resourcesConversion[variables.resourceCostType].CurrentValue -= currentCost;
        currentCost = GenerateCostOnLevel(lvl);
        ResourceManager.Instance.resourcesConversion[variables.resourceCostType].UpdateUpgradable();
    }
}
