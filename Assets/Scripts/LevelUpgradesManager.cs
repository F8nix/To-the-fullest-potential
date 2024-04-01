using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Resource
{
    Potentia,
    Vidya
}



public class LevelUpgradesManager : MonoBehaviour
{
    public static float vidyaBase = 12;
    public static float vidyaCapacityBase = 1;

    public static float vidyaCapacityMultiplier = 1.2f;
    public static float potentiaBase = 0;

    public static LevelUpgradesManager Instance {get; private set;}
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public float GenerateCapacityOnLevel(int level, Resource resource) {
        if(resource == Resource.Vidya){     
            return (float) Math.Ceiling((vidyaBase * vidyaCapacityBase) * Math.Pow(vidyaCapacityMultiplier, level-1));
        }
        return -1;
    }

    public bool CanLevelUp(float currentResource, float requiredResource, float resourceChange, bool levelAboveRequirement) {
        return levelAboveRequirement
            ? ((currentResource + resourceChange) >= requiredResource)
            : ((currentResource + resourceChange) <= requiredResource)
            ;   
    }

    public void LevelUp(ref int level, Resource resource, ref float capacity, ref float currentResource, bool levelAboveRequirement, bool currentResetOnLvling){
        level++;
        capacity = GenerateCapacityOnLevel(level, resource);
        if(currentResetOnLvling && levelAboveRequirement){
            currentResource = 0;
        } else if (currentResetOnLvling && !levelAboveRequirement){
            currentResource = capacity;
        }
    }

    //-> do resource
}
