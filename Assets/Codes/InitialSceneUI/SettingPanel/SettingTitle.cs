using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
public class SettingTitle : MonoBehaviour
{
    public Text Title;
    public Dropdown InitialLocaleSelector;
    // Start is called before the first frame update
    void Start()
    {
        InitialSceneLocale locale = InitialLocaleSelector.GetComponent<InitialSceneLocale>();
        string label = locale.GetLocaleWord("SettingTitle");
        Title.text = label;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocaleUpdate(InitialSceneLocale locale)
    {
        string label = locale.GetLocaleWord("SettingTitle");
        Title.text = label + ":";
    }
}
