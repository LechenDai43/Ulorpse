using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    Text LoadGameLabel;
    void Start()
    {
        LoadGameLabel = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LocaleUpdate(InitialSceneLocale locale)
    {
        string label = locale.GetLocaleWord("Setting");
        LoadGameLabel.text = label;
    }
}
