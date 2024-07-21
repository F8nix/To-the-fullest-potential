using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public enum CharmedVariable
{
    CurrentValue
}

public class Spell : MonoBehaviour
{
    
    public int Lvl {get; set;} = 0;

    //public UnityEvent<Resource> sliderUpdate;

    public SpellsSO spellsData;

    public List<SpellResourceCost> spellResourcesCost;
    public List<SpellInfluence> spellResourcesInfluence;

    public List<ResourceType> upgradeResources;

    //public SpellButtonController upgradeButtonController;

    void Start()
    {
        //Cost = GenerateCostOnLevel(Lvl);
        spellResourcesCost = new();
        spellResourcesInfluence = new();
        upgradeResources = new();
        foreach (var variable in spellsData.spellVariables)
        {
            spellResourcesCost.Add(new SpellResourceCost(variable, spellsData));
            upgradeResources.Add(variable.resourceCostType);
        }
        foreach (var target in spellsData.spellTarget)
        {
            spellResourcesInfluence.Add(new SpellInfluence(target, spellsData));
        }
        if(upgradeResources == null){
            Debug.Log("spellResources");
        }
        if(this.GetComponent<Button>() == null){
            Debug.Log("button");
        }
        //spellButtonController.upgradeButtons.Add(upgradeResources, this.GetComponent<Button>());
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
    [CustomEditor(typeof(Spell))]
    class UpgradeEditor : Editor {
        public override void OnInspectorGUI()
        {
            var spell = (Spell)target;
            if (spell == null) return;

            //var spellDebugAddLvls = 1;

            if(GUILayout.Button("CastSpell")){
                if(spell.CanCast()){
                    spell.CastSpell();
                }
                
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public bool CanCast() {
        return spellResourcesCost.All(spellResource =>{ 
        return spellResource.currentCost < ResourceManager.Instance.resourcesConversion[spellResource.variables.resourceCostType].CurrentValue;
        //&& ResourceManager.Instance.resourcesConversion[upgradeResource.variables.resourceCostType].CurrentValue - upgradeResource.currentCost > 0;
    });  
    }

    public void CastSpell() {
        foreach (var spellResource in spellResourcesCost)
        {
            spellResource.UpdateOnCast();
        }
        foreach (var spellResource in spellResourcesInfluence)
        {
            spellResource.UpdateOnCast();
        }
    }

    public void CastOnClick() {

        if(CanCast()){
            CastSpell();
        }
    }
    /*

    trzeba znaczące zmiany tu wprowadzić żeby rzeczy sensownie się updatowały LUB w ogóle to nie tutaj będzie

    private void LevelUp(){
        Lvl++;
        foreach (var spellResource in spellResourcesCost)
        {
            spellResource.UpdateOnLevelUp(this.Lvl);
        }
        foreach (var spellResource in spellResourcesInfluence)
        {
            spellResource.UpdateOnLevelUp(this.Lvl);
        }
        
    }

    private void AddLvl(int Lvl) {
        this.Lvl += Lvl;
        foreach (var spellResource in spellResourcesCost)
        {
            spellResource.UpdateOnLevelUp(this.Lvl);
        }
        foreach (var spellResource in spellResourcesInfluence)
        {
            spellResource.UpdateOnLevelUp(this.Lvl);
        }
        
    }
    */
}
