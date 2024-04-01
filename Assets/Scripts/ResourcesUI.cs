using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public Slider firstResourceSlider;
    public TextMeshProUGUI firstResourceValues;
    public Vidya vidya;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        firstResourceSlider.value = vidya.GetCurrentVidya();
        firstResourceSlider.maxValue = vidya.GetVidyaCapacity(); //zmienic na event co lvlUp
        firstResourceValues.text = $"{vidya.GetCurrentVidya()}/{vidya.GetVidyaCapacity()}";
    }
}
