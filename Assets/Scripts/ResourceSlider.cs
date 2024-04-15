using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSlider : MonoBehaviour
{
    public Slider resourceSlider;
    public TextMeshProUGUI resourceValues;
    public ResourceType sliderResource;

    public void SliderUpdate(){
        Debug.Log("Slider update!");
        for (int i = 0; i < ResourceSliderController.Instance.resourcesList.Count; i++) {
            Debug.Log("For state: "+i);
            if(sliderResource == ResourceSliderController.Instance.resourcesList[i].GetResourceType()){
                resourceSlider.maxValue = ResourceSliderController.Instance.resourcesList[i].Capacity;
                resourceSlider.value = ResourceSliderController.Instance.resourcesList[i].CurrentValue;
                resourceValues.text = $"{ResourceSliderController.Instance.resourcesList[i].CurrentValue}/{ResourceSliderController.Instance.resourcesList[i].Capacity}";
            }
        }
    }
}
