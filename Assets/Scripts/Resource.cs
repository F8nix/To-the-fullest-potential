using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;
using System.Runtime.Serialization.Json;

public enum ResourceType
{
    Potentia,
    Vidya
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
    public static float potentiaBase = 0;

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
        }
    }
    
    private float currentValue = 0;
    public float Capacity {get; set;} = 0;
    public int Lvl {get; set;} = 1;

    void Start()
    {
        Capacity = GenerateCapacityOnLevel(Lvl, ResourceType.Vidya);
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
                Debug.Log("Current resource " + resource.currentValue + " current tool lvl: " + resource.Lvl + " current capacity " + resource.Capacity);
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public static float GenerateCapacityOnLevel(int level, ResourceType resource) {
        if(resource == ResourceType.Vidya){     
            return (float) Math.Ceiling((vidyaBase * vidyaCapacityBase) * Math.Pow(vidyaCapacityMultiplier, level-1));
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
}
