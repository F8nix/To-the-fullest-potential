using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ResourceManager Instance { get; private set; }
    public Dictionary<ResourceType, Resource> resourcesConversion;

    public List<Resource> resourcesList;
    //public List<ResourceType> resourceTypesList;
    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    private void Start() {
        resourcesConversion = new();
        for (int resourceID = 0; resourceID < resourcesList.Count; resourceID++){
            resourcesConversion.Add(resourcesList[resourceID].GetResourceType(), resourcesList[resourceID]);
        }
    }
}
