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

    public void SliderUpdate(Resource resource){
        if(resource.GetResourceType() != sliderResource) return;

        resourceSlider.maxValue = resource.Capacity;
        resourceSlider.value = resource.CurrentValue;
        resourceValues.text = $"{resource.CurrentValue}/{resource.Capacity}";
    }
}
