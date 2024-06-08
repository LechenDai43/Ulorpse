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

    public TextAsset ElixirDescriptionFileCN, ElixirDescriptionFileEN, ElixirDescriptionFileJP, ElixirDescriptionFileNY;
    public Dictionary<string, string> ElixirDescriptions;    
    public List<string> ElixirDescriptionVariableList = new List<string>();
    public TextAsset ElixirDescriptionVariable;

     void Start()
    {
        if (GameManager.NameLocaleManager == null)
        {
            GameManager.NameLocaleManager = this;
        }
        else
        {
            Destroy(gameObject);
            return ;
        }
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileCN);
                ElixirDescriptions = LocaleStringParser.ParseLocaleFromTextAsset(ElixirDescriptionFileCN);
                break;
            case Locales.English:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileEN);
                ElixirDescriptions = LocaleStringParser.ParseLocaleFromTextAsset(ElixirDescriptionFileEN);
                break;
            case Locales.Japanese:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileJP);
                ElixirDescriptions = LocaleStringParser.ParseLocaleFromTextAsset(ElixirDescriptionFileJP);
                break;
            case Locales.Chichewa:
                CommonElixirNames = LocaleStringParser.ParseLocaleFromTextAsset(CommonElixirNameFileNY);
                ElixirDescriptions = LocaleStringParser.ParseLocaleFromTextAsset(ElixirDescriptionFileNY);
                break;
        }
        
        CommonElixirNameVariableList = LocaleStringParser.LoadLocalVariableNames(CommonElixirNameVariable);
        ElixirDescriptionVariableList = LocaleStringParser.LoadLocalVariableNames(ElixirDescriptionVariable);
    }
}
