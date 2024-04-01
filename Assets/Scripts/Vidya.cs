using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;

public class Vidya : MonoBehaviour
{
    private float currentVidya = 0;
    private float vidyaCapacity = 0;

//>>leveling rules
    private bool lvlAbove = true;
    private bool currentReset= true;

    [SerializeField] private Resource resource;

//>>-leveling rules

    private int lvl = 1;

    void Start()
    {
        vidyaCapacity = LevelUpgradesManager.Instance.GenerateCapacityOnLevel(lvl, Resource.Vidya);
    }

    void Update()
    {
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Vidya))]
    class VidyaEditor : Editor {
        public override void OnInspectorGUI()
        {
            var vidya = (Vidya)target;
            if (vidya == null) return;

            var vidyaDebugAddValue = 3;

            if(GUILayout.Button("AddToCurrent")){
                
                if(LevelUpgradesManager.Instance.CanLevelUp(vidya.currentVidya, vidya.vidyaCapacity, vidyaDebugAddValue, vidya.lvlAbove)){
                    LevelUpgradesManager.Instance.LevelUp(ref vidya.lvl, vidya.resource, ref vidya.vidyaCapacity,
                    ref vidya.currentVidya, vidya.lvlAbove, vidya.currentReset); // -> funkcja set
                } else {
                    vidya.currentVidya += vidyaDebugAddValue;
                }
                Debug.Log("Current vidya " + vidya.currentVidya + " current tool lvl: " + vidya.lvl + " current capacity " + vidya.vidyaCapacity);
            }

            DrawDefaultInspector();
        }
    }

    #endif

    public float GetCurrentVidya() {
        return currentVidya;
    }

    public float GetVidyaCapacity() {
        return vidyaCapacity;
    }

    //zmiana na resource ogólny
}
