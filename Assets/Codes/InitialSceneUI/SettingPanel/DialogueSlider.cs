using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class DialogueSlider : MonoBehaviour
{
    public Text Label, Number;
    public Dropdown InitialLocaleSelector;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        InitialSceneLocale locale = InitialLocaleSelector.GetComponent<InitialSceneLocale>();
        string label = locale.GetLocaleWord("Dialogue");
        Label.text = label; slider = gameObject.GetComponent<Slider>();
        slider.wholeNumbers = true;
        slider.maxValue = 100;
        slider.minValue = 0;
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            if (GameManager.GetDialogueVolume() == null)
            {
                GameManager.SetDialogueVolume(100);

            }

            slider.value = GameManager.GetDialogueVolume();
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }

        Number.text = ((int)slider.value).ToString(); ;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocaleUpdate(InitialSceneLocale locale)
    {
        string label = locale.GetLocaleWord("Dialogue");
        Label.text = label + ":";
    }

    public void VolumeUpdate()
    {
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            GameManager.SetDialogueVolume((int)slider.value);
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
        Number.text = ((int)slider.value).ToString();
    }
}
