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
    public GameObject InitialPanel, SettingPanel;
    public Dropdown InitialLocaleSelector;
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

    public void OnLaunchSetting()
    {
        SettingPanel.SetActive(true);
        Selectable[] selectables = InitialPanel.GetComponentsInChildren<Selectable>();

        // Disable interactability for each Selectable object
        foreach (Selectable selectable in selectables)
        {
            selectable.interactable = false;
        }
        InitialLocaleSelector.interactable = false;
    }
}
