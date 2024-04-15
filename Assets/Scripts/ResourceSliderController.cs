using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceSliderController : MonoBehaviour
{
    public ResourceSlider firstSlider;
    public ResourceSlider secondSlider;

    public List<Resource> resourcesList;

    //private List<Resource> activeResources;

    public static ResourceSliderController Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() {
        if (Instance != null && Instance != this) 
    { 
        Destroy(this); 
    } 
    else 
    { 
        Instance = this; 
    } 
    }

    
}
