using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class MusicSlider : MonoBehaviour
{
    public Text Label, Number;
    public Dropdown InitialLocaleSelector;
    public BackgroundAudio Audio;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        InitialSceneLocale locale = InitialLocaleSelector.GetComponent<InitialSceneLocale>();
        string label = locale.GetLocaleWord("Music");
        Label.text = label;
        slider = gameObject.GetComponent<Slider>();
        slider.wholeNumbers = true;
        slider.maxValue = 100;
        slider.minValue = 0;
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            if (GameManager.GetMusicVolume() == null)
            {
                GameManager.SetMusicVolume(100);

            }

            slider.value = GameManager.GetMusicVolume();
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
        string label = locale.GetLocaleWord("Music");
        Label.text = label + ":";
    }

    public void VolumeUpdate()
    {
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            GameManager.SetMusicVolume((int)slider.value);
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
        Number.text = ((int)slider.value).ToString();
        Audio.UpdateVolume(slider.value / 100);
    }
}
