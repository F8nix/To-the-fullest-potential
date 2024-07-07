using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class UpgradeButtonController : MonoBehaviour
{
    public Dictionary<List<ResourceType>, Button> upgradeButtons;
    public HashSet<Button> activeButtons;

    public GameObject grid;

    //private bool doneOnce = false; //debug tool

    void Start()
    {
        upgradeButtons = new Dictionary<List<ResourceType>, Button>();
        activeButtons = new();
    }


    void Update()
    {
        /*
        if(!doneOnce){
        for (int i = 0; i < upgradeButtons.Count; i++){
            Debug.Log("Key: "+upgradeButtons.ElementAt(i).Key+", Value: "+upgradeButtons.ElementAt(i).Value);
        }
        doneOnce = true;
        }
        */ //debug tool
    }

    public void UpdateActiveButtonsList(ResourceType firstResource){
        
        foreach (var button in upgradeButtons)
        {
            foreach (var resourceType in button.Key)
            {
                if(resourceType == firstResource){
                    activeButtons.Add(button.Value);
                }
            }
        }
    }
    /*
    public void UpdateActiveButtonsList(ResourceType firstResource, ResourceType secondResource){

        foreach (var button in upgradeButtons)
        {
            foreach (var resourceType in button.Key)
            {
                if(resourceType == firstResource || resourceType == secondResource){
                    activeButtons.Add(button.Value);
                }
            }
        }
    }
    */
    public void UpdateGrid() {
        foreach (var button in activeButtons)
        {
            button.transform.SetParent(grid.transform); //false is used fort UI, but it hid buttons so I didn't use it
        }
    }

    public void ClearActiveButtonsList(){
        grid.transform.DetachChildren();
        activeButtons = new();
    }
}
