using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public enum UpgradedVariable
{
    Capacity,
    ReplenishRate
}

public class Upgrade : MonoBehaviour
{
    
    public int Lvl {get; set;} = 0;

    //public UnityEvent<Resource> sliderUpdate;

    public UpgradesSO upgradesData;

    public List<UpgradeResourceCost> upgradeResourcesCost;
    public List<UpgradeResourceInfluence> upgradeResourcesInfluence;

    void Start()
    {
        //Cost = GenerateCostOnLevel(Lvl);
        upgradeResourcesCost = new();
        upgradeResourcesInfluence = new();
        foreach (var variable in upgradesData.upgradeVariables)
        {
            upgradeResourcesCost.Add(new UpgradeResourceCost(variable, Lvl, upgradesData));
        }
        foreach (var target in upgradesData.upgradeTarget)
        {
            upgradeResourcesInfluence.Add(new UpgradeResourceInfluence(target, Lvl, upgradesData));
        }
        //interfejs, żeby nie było podwójnych for eachów/jednej listy
        
        /*
        if (sliderUpdate == null)
            sliderUpdate = new UnityEvent<Resource>();

        sliderUpdate.AddListener(ResourceSliderController.Instance.firstSlider.SliderUpdate);
        sliderUpdate.AddListener(ResourceSliderController.Instance.secondSlider.SliderUpdate);

        sliderUpdate.Invoke(this);
        */
    }

    void Update()
    {
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Upgrade))]
    class UpgradeEditor : Editor {
        public override void OnInspectorGUI()
        {
            var upgrade = (Upgrade)target;
            if (upgrade == null) return;

            var upgradeDebugAddLvls = 1;

            if(GUILayout.Button("AddToCurrent")){
                if(upgrade.CanLevelUp()){
                    upgrade.AddLvl(upgradeDebugAddLvls);
                }
                
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public bool CanLevelUp() {
        return upgradeResourcesCost.All(upgradeResource =>{ 
        return upgradeResource.currentCost < ResourceManager.Instance.resourcesConversion[upgradeResource.variables.resourceCostType].CurrentValue
        && ResourceManager.Instance.resourcesConversion[upgradeResource.variables.resourceCostType].CurrentValue - upgradeResource.currentCost > 0;
    });  
    }

    public void LevelUp(){
        Lvl++;
        foreach (var upgradeResource in upgradeResourcesCost)
        {
            upgradeResource.UpdateOnLevelUp(this.Lvl);
        }
        foreach (var upgradeResource in upgradeResourcesInfluence)
        {
            upgradeResource.UpdateOnLevelUp(this.Lvl);
        }
    }

    private void AddLvl(int Lvl) {
        this.Lvl += Lvl;
        foreach (var upgradeResource in upgradeResourcesCost)
        {
            upgradeResource.UpdateOnLevelUp(this.Lvl);
        }
        foreach (var upgradeResource in upgradeResourcesInfluence)
        {
            upgradeResource.UpdateOnLevelUp(this.Lvl);
        }
    }
}
