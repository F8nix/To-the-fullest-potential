using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "ScriptableObjects/ResourceSO")]
public class ResourcesSO : ScriptableObject
{
    //>> Lvling rules
    public bool lvlAbove;
    public bool currentValueReset;
    public ResourceType resourceType;

    public bool autoLvlUp;
    //->>

    public Color color;
    public float resourceBase;
    public float resourceCapacityBase;
    public float resourceCapacityMultiplier;
}
