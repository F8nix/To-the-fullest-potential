using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class Upgrade : MonoBehaviour
{
    
    public int Lvl {get; set;} = 1;

    //public UnityEvent<Resource> sliderUpdate;

    public UpgradesSO upgradesData;

    public List<UpgradeResource> upgradeResources;

    void Start()
    {
        //Cost = GenerateCostOnLevel(Lvl);
        upgradeResources = new List<UpgradeResource>();
        foreach (var variable in upgradesData.upgradeVariables)
        {
            upgradeResources.Add(new UpgradeResource(variable, Lvl, upgradesData));
        }
        
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
                upgrade.SetLvl(upgradeDebugAddLvls);
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public bool CanLevelUp() {
        return upgradeResources.All(upgradeResource => upgradeResource.currentCost < upgradeResource.GetResourceCurrentValue(upgradeResource.variables.upgradeResourceType));  
    }

    public void LevelUp(){
        Lvl++;
        foreach (var upgradeResource in upgradeResources)
        {
            upgradeResource.GenerateCostOnLevel(Lvl);
            upgradeResource.GenerateInfluenceOnLevel(Lvl);
        }
    }

    private void SetLvl(int Lvl) {
        this.Lvl = Lvl;
        foreach (var upgradeResource in upgradeResources)
        {
            upgradeResource.GenerateCostOnLevel(Lvl);
            upgradeResource.GenerateInfluenceOnLevel(Lvl);
        }
    }
}
