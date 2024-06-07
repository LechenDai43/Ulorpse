using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using System;

public class NameLocale : MonoBehaviour
{
    private static NameLocale _instance;
    public static NameLocale Instance
    {
        get { return _instance; }
    }

    // Common elixir name locales
    public TextAsset CommonElixirNameFileCN, CommonElixirNameFileEN, CommonElixirNameFileJP, CommonElixirNameFileNY;
    public Dictionary<string, string> CommonElixirNames;    
    public List<string> CommonElixirNameVariableList = new List<string>();
    public TextAsset CommonElixirNameVariable;

     void Start()
    {
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileCN);
                break;
            case Locales.English:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileEN);
                break;
            case Locales.Japanese:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileJP);
                break;
            case Locales.Chichewa:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileNY);
                break;
        }
        
        CommonElixirNameVariableList = LocaleStringParser.LoadLocalVariableNames(CommonElixirNameVariable);


        if (GameManager.NameLocaleManager == null)
        {
            GameManager.NameLocaleManager = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance of GameManager exists
        }
    }
}
