using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInfluence
{
    public float currentInfluence = 0;

    public SpellsSO data;
    public SpellInfluenceData targetData;

    public SpellInfluence( SpellInfluenceData targetData, SpellsSO data) {
        this.data = data;
        this.targetData = targetData;
        currentInfluence = GenerateInfluenceOnCast();
    }

    public float GenerateInfluenceOnCast() {
        return Mathf.Ceil((1/data.spellBase * targetData.influenceBase) * targetData.influenceMultiplier);
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public void UpdateOnCast() {
        
        Debug.Log(currentInfluence + "Spell Influence");
        
        currentInfluence = GenerateInfluenceOnCast();
        ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].CurrentValue += currentInfluence;
        //ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].UpdateUpgradable();
    }

    /*

    trzeba znaczące zmiany tu wprowadzić żeby rzeczy sensownie się updatowały LUB w ogóle to nie tutaj będzie
    
    public float GenerateInfluenceOnLevel(int level) {
        return MathF.Round((data.spellBase * targetData.influenceBase) * Mathf.Pow(targetData.influenceMultiplier, level), 2);
        //1/Mathf.Pow(2, spellsData.spellBase)
    }

    public void UpdateOnLevelUp(int lvl) {
        
        Debug.Log(currentInfluence + "Influence");
        
        ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].capacityUpgradesMultiplier = currentInfluence;
        currentInfluence = GenerateInfluenceOnLevel(lvl);
        ResourceManager.Instance.resourcesConversion[targetData.influencedResourceType].UpdateUpgradable();
    }

    */
}
