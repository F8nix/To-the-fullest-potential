using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public enum ResourceType
{
    Potentia,
    Vidya,
    RandomThird
}

public class Resource : MonoBehaviour
{
    public float CurrentValue
    {
        get {return currentValue;}
        set {
            if(resourcesData.autoLvlUp && CanLevelUp(value)){
                LevelUp();
            } else if (value >= Capacity) {
                currentValue = Capacity;
            } else {
                currentValue = value;
            }
            sliderUpdate?.Invoke(this);
        }
    }
    
    private float currentValue = 0;
    public float Capacity {get; set;} = 0;
    public int Lvl {get; set;} = 1;

    public float GeneratingRate {get; set;} = 0;

    public float capacityUpgradesMultiplier = 1;

    public UnityEvent<Resource> sliderUpdate;

    public ResourcesSO resourcesData;

    void Start()
    {
        GeneratingRate = resourcesData.generatingRate;
        Capacity = GenerateCapacity();
        
        //sliderUpdate.AddListener(secondSlider.SliderUpdate);

        if (sliderUpdate == null)
            sliderUpdate = new UnityEvent<Resource>();

        sliderUpdate.AddListener(ResourceSliderController.Instance.firstSlider.SliderUpdate);
        sliderUpdate.AddListener(ResourceSliderController.Instance.secondSlider.SliderUpdate);

        sliderUpdate?.Invoke(this);
    }

    void Update()
    {
        HandleGenerating();
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Resource))]
    class ResourceEditor : Editor {
        public override void OnInspectorGUI()
        {
            var resource = (Resource)target;
            if (resource == null) return;

            var resourceDebugAddValue = 3;

            if(GUILayout.Button("AddToCurrent")){
                resource.CurrentValue += resourceDebugAddValue;
                //Debug.Log("Current resource " + resource.currentValue + " current tool lvl: " + resource.Lvl + " current capacity " + resource.Capacity);
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public float GenerateCapacity() {
        return Mathf.Ceil((resourcesData.resourceBase * resourcesData.resourceCapacityBase) *
        Mathf.Pow(resourcesData.resourceCapacityMultiplier, Lvl-1) * capacityUpgradesMultiplier);
    }

    public bool CanLevelUp(float newResourceValue) {
        return resourcesData.lvlAbove
            ? (newResourceValue >= Capacity)
            : (newResourceValue <= Capacity)
            ;   
    }

    public void LevelUp(){
        Lvl++;
        Capacity = GenerateCapacity();
        Debug.Log(Capacity + "Capacity");
        if(resourcesData.currentValueReset && resourcesData.lvlAbove){
            CurrentValue = 0;
        } else if (resourcesData.currentValueReset && !resourcesData.lvlAbove){
            CurrentValue = Capacity;
        }
    }

    public ResourceType GetResourceType() {
        return resourcesData.resourceType;
    }

    public void UpdateUpgradable() {
        Capacity = GenerateCapacity();
        sliderUpdate?.Invoke(this);
    }

    public void HandleGenerating() {
        if(resourcesData.isGenerating){
            CurrentValue += GeneratingRate * Time.deltaTime;
        }
    }
}
