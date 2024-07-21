using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeResourceInfluence
{
    public float currentInfluence = 0;

    public UpgradesSO data;
    public UpgradeInfluenceData targetData;

    public UpgradeResourceInfluence( UpgradeInfluenceData targetData, int lvl, UpgradesSO data) {
        this.data = data;
        this.targetData = targetData;
        currentInfluence = GenerateInfluenceOnLevel(lvl);
    }
    
    public float GenerateInfluenceOnLevel(int level) {
        return MathF.Round((data.upgradeBase * targetData.influenceBase) * Mathf.Pow(targetData.influenceMultiplier, level), 2);
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public void UpdateOnLevelUp(int lvl) {
        
        Debug.Log(currentInfluence + "Influence");
        
        ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].capacityUpgradesMultiplier = currentInfluence;
        currentInfluence = GenerateInfluenceOnLevel(lvl);
        ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].UpdateUpgradable();
    }
}
