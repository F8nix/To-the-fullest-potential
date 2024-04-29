using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ResourceSliderController : MonoBehaviour
{
    public ResourceSlider firstSlider;
    public ResourceSlider secondSlider;

    private List<Resource> activeResources;

    public static ResourceSliderController Instance { get; private set; }

    public CustomToggleGroup customToggleGroup;

    public UnityEvent<HashSet<ResourceToggle>> SetSlider;


    // Start is called before the first frame update
    void Start()
    {
        if(SetSlider != null){
            SetSlider = new UnityEvent<HashSet<ResourceToggle>>();
        }

        SetSlider.AddListener(OnToggleChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    private void OnToggleChange(HashSet<ResourceToggle> activeToggles){
        activeResources = new();
        var i = 0;
        foreach (var toggle in activeToggles)
        {
            var resource = ResourceManager.Instance.resourcesConversion[toggle.resourceType];
            activeResources.Add(resource);
            //Debug.Log(ResourceManager.Instance.resourcesConversion[toggle.resourceType]);
            if(i == 0){
                firstSlider.sliderResource = toggle.resourceType;
                firstSlider.SliderUpdate(resource);
                firstSlider.resourceSlider.fillRect.GetComponentInChildren<Image>().color = resource.resourcesData.color;
            } else {
                secondSlider.sliderResource = toggle.resourceType;
                secondSlider.SliderUpdate(resource);
                secondSlider.resourceSlider.fillRect.GetComponentInChildren<Image>().color = resource.resourcesData.color;
            }
            i++;
        }
        if(activeResources.Count == 0){
            Debug.Log("0");
            firstSlider.resourceSlider.gameObject.SetActive(false);
            secondSlider.resourceSlider.gameObject.SetActive(false);
        } else if(activeResources.Count == 1){
            Debug.Log("1");
            firstSlider.resourceSlider.gameObject.SetActive(true);
            secondSlider.resourceSlider.gameObject.SetActive(false);
        }  else {
            Debug.Log("2");
            firstSlider.resourceSlider.gameObject.SetActive(true);
            secondSlider.resourceSlider.gameObject.SetActive(true);
        }
    }
}
