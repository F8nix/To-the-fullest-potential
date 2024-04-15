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
//>>leveling rules
    private bool lvlAbove = true;
    private bool currentValueReset = true;

    [SerializeField] private ResourceType resource;

//>>-leveling rules

//>> bases and multipliers

    public static float vidyaBase = 12;
    public static float vidyaCapacityBase = 1;

    public static float vidyaCapacityMultiplier = 1.2f;

    public static float potentiaBase = 33;
    public static float potentiaCapacityBase = 1;

    public static float potentiaCapacityMultiplier = 1.2f;

    public static float randomThirdBase = 100;
    public static float randomThirdCapacityBase = 1.5f;

    public static float randomThirdCapacityMultiplier = 1.2f;

//->> bases
    public float CurrentValue
    {
        get {return currentValue;}
        set {
            if(CanLevelUp(value)){
                LevelUp();
            } else {
                currentValue = value;
            }
            sliderUpdate.Invoke();
        }
    }
    
    private float currentValue = 0;
    public float Capacity {get; set;} = 0;
    public int Lvl {get; set;} = 1;

    public UnityEvent sliderUpdate;

    void Start()
    {
        Capacity = GenerateCapacityOnLevel(Lvl, ResourceType.Vidya);
        
        //sliderUpdate.AddListener(secondSlider.SliderUpdate);

        if (sliderUpdate == null)
            sliderUpdate = new UnityEvent();

        sliderUpdate.AddListener(ResourceSliderController.Instance.firstSlider.SliderUpdate);
        sliderUpdate.AddListener(ResourceSliderController.Instance.secondSlider.SliderUpdate);

        sliderUpdate.Invoke();
    }

    void Update()
    {
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

    public static float GenerateCapacityOnLevel(int level, ResourceType resource) {
        if(resource == ResourceType.Vidya){     
            return (float) Math.Ceiling((vidyaBase * vidyaCapacityBase) * Math.Pow(vidyaCapacityMultiplier, level-1));
        }
        if(resource == ResourceType.Potentia){     
            return (float) Math.Ceiling((potentiaBase * potentiaCapacityBase) * Math.Pow(potentiaCapacityMultiplier, level-1));
        }
        if(resource == ResourceType.RandomThird){     
            return (float) Math.Ceiling((randomThirdBase * randomThirdCapacityBase) * Math.Pow(randomThirdCapacityMultiplier, level-1));
        }
        return -1;
    }

    public bool CanLevelUp(float newResourceValue) {
        return lvlAbove
            ? (newResourceValue >= Capacity)
            : (newResourceValue <= Capacity)
            ;   
    }

    public void LevelUp(){
        Lvl++;
        Capacity = GenerateCapacityOnLevel(Lvl, resource);
        if(currentValueReset && lvlAbove){
            CurrentValue = 0;
        } else if (currentValueReset && !lvlAbove){
            CurrentValue = Capacity;
        }
    }

    public ResourceType GetResourceType() {
        return resource;
    }
}
