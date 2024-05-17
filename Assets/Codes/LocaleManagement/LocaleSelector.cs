using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class LocaleSelector : MonoBehaviour
{
    public Dropdown selector;

    public Logo logo;
    public LoadGameButton loadGameButton;
    public NewGameButton newGameButton;
    public SettingButton settingButton;

    // Start is called before the first frame update
    void Start()
    {
        // Access GameManager instance
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            if (GameManager.GetLocale() == null || (int)GameManager.GetLocale() == 0)
            {
                GameManager.SetLocale(Locales.Chinese);

            }

            selector.value = (int)GameManager.GetLocale();
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }

        

    }

    // Update is called once per frame
    public void OnLocaleSelect()
    {
        InitialSceneLocale locale = gameObject.GetComponent<InitialSceneLocale>();
        GameManager.SetLocale((Locales)selector.value);
        logo.LocaleUpdate();
        loadGameButton.LocaleUpdate(locale);
        newGameButton.LocaleUpdate(locale);
        settingButton.LocaleUpdate(locale);
}
}

public enum Locales
{
    Chinese,
    English,
    Japanese
}