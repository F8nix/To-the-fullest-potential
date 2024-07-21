using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SpellResourceCost
{
    
    public float currentCost = 0;

    

    public SpellsSO data;
    public SpellVariables variables;


    public SpellResourceCost (SpellVariables variables, SpellsSO data) {
        this.data = data;
        this.variables = variables;
        currentCost = GenerateCostOnCast();
    }

    public float GenerateCostOnCast() {
        return Mathf.Ceil((1/data.spellBase * variables.costBase) * variables.costMultiplier);
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public void UpdateOnCast() {
        Debug.Log(currentCost + "Spell Cost");
        currentCost = GenerateCostOnCast();
        ResourceManager.Instance.resourcesConversion[variables.resourceCostType].CurrentValue -= currentCost;
        //ResourceManager.Instance.resourcesConversion[variables.resourceCostType].UpdateUpgradable();
    }

    /*

    trzeba znaczące zmiany tu wprowadzić żeby rzeczy sensownie się updatowały LUB w ogóle to nie tutaj będzie

    public float GenerateCostOnLevel(int level) {
        return Mathf.Ceil((1/data.spellBase * variables.costBase) * Mathf.Pow(variables.costMultiplier, level+1));
        //1/Mathf.Pow(2, upgradesData.upgradeBase)
    }

    public void UpdateOnLevelUp(int lvl) {
        
        Debug.Log(currentCost + "Cost");
        
        ResourceManager.Instance.resourcesConversion[variables.resourceCostType].CurrentValue -= currentCost;
        currentCost = GenerateCostOnLevel(lvl);
        ResourceManager.Instance.resourcesConversion[variables.resourceCostType].UpdateUpgradable();
    }

    */
}
