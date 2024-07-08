using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using Unity.Profiling;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UButtonEnableOrParent
{
  List<Button> buttons = new();
  Transform parent;
  int count = 500;
  int measurements = 50;


  // Called before each test, so we can get a fresh set of buttons each time, just in case
  // There's also OneTimeSetUp that would probably be better for this, but as an example this is nice.
  [UnitySetUp]
  public IEnumerator Setup()
  {
    for (int i = 0; i < count; i++)
    {
      var go = new GameObject("Button" + i);
      var button = go.AddComponent<Button>();
      var goText = new GameObject("Text" + i);
      var text = goText.AddComponent<Text>();
      goText.transform.SetParent(go.transform);
      buttons.Add(button);
    }
    parent = new GameObject("Parent").transform;
    yield return new WaitForEndOfFrame(); // calls awake
    yield return new WaitForEndOfFrame(); // calls start
  }

  // Called after each test, ideally should completely revert Setup.
  [UnityTearDown]
  public IEnumerator TearDown()
  {
    buttons.ForEach(button => GameObject.Destroy(button.gameObject));
    buttons.Clear();
    GameObject.Destroy(parent.gameObject);
    parent = null;
    yield return new WaitForEndOfFrame();
  }

  [UnityTest, Performance]
  public IEnumerator SetReparenting()
  {
    var sampleGroup = new SampleGroup("Reparenting", SampleUnit.Millisecond);
    if (parent == null) throw new Exception("parent is null");
    using (Measure.Frames().ProfilerMarkers(new[] { "UnityEngine.Transform.SetParent" }).SampleGroup(sampleGroup).Scope("Reparenting"))
    {
      for (int i = 0; i < measurements; i++)
      {

        foreach (var button in buttons)
        {
          button.transform.SetParent(parent);
        }
        yield return new WaitForEndOfFrame();

        foreach (var button in buttons)
        {
          button.transform.SetParent(null);
        }
        yield return new WaitForEndOfFrame();
      }
    }
  }


  [UnityTest, Performance]
  public IEnumerator SetActive()
  {
    var sampleGroup = new SampleGroup("SetActive", SampleUnit.Millisecond);

    using (Measure.Frames().ProfilerMarkers(new[] { "UnityEngine.GameObject.SetActive" }).SampleGroup(sampleGroup).Scope("SetActive"))
    {
      for (int i = 0; i < measurements; i++)
      {
        foreach (var button in buttons)
        {
          button.gameObject.SetActive(true);
        }
        yield return new WaitForEndOfFrame();
        foreach (var button in buttons)
        {
          button.gameObject.SetActive(false);
        }
        yield return new WaitForEndOfFrame();
      }
    }
  }
}