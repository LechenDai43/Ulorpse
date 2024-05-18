using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class CloseSettingButton : MonoBehaviour
{
    public GameObject InitialPanel, SettingPanel;
    public Dropdown InitialLocaleSelector;
    public Text Label;

    // Start is called before the first frame update
    void Start()
    {
        if (Label != null && InitialLocaleSelector != null)
        {
            InitialSceneLocale locale = InitialLocaleSelector.GetComponent<InitialSceneLocale>();
            string label = locale.GetLocaleWord("Back");
            Label.text = label;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocaleUpdate(InitialSceneLocale locale)
    {
        if (Label != null)
        {
            string label = locale.GetLocaleWord("Back");
            Label.text = label;
        }
    }

    public void OnCloseSetting()
    {
        Selectable[] selectables = InitialPanel.GetComponentsInChildren<Selectable>();

        // Disable interactability for each Selectable object
        foreach (Selectable selectable in selectables)
        {
            selectable.interactable = true;
        }
        InitialLocaleSelector.interactable = true;

        SettingPanel.SetActive(false);
    }
}
