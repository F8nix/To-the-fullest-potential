using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggleGroup : MonoBehaviour
{
    public HashSet<Toggle> activeToggles;
    public List<Toggle> toggles;
    // Start is called before the first frame update
    void Start()
    {
        activeToggles = new();

        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate {
            AddToggleOnChange(toggle);
            });
        }

        //>> rip rozwiązanie trochę, ale potentia i vidya MUSZĄ być na pierszych dwóch pozycjach

        toggles[0].isOn = true;
        toggles[1].isOn = true;

        //->>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToggleOnChange(Toggle toggle) {
        //Debug.Log(activeToggles.Count);
        if(toggle.isOn){
            activeToggles.Add(toggle);
        } else {
            activeToggles.Remove(toggle);
        }
        if(activeToggles.Count == 2) {
            foreach (var t in toggles)
            {
                if(!t.isOn){
                    t.enabled = false;
                }
            }
        } else {
            foreach (var t in toggles)
            {
                t.enabled = true;
            }
        }
    }
}
