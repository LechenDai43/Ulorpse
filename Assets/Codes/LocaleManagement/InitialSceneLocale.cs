using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSceneLocale : MonoBehaviour
{
    public TextAsset textChineseFile, textEnglishFile, textJapaneseFile, textChichewaFile;
    public Dictionary<string, string> chineseLocale, englishLocale, japaneseLocale, chichewaLocale;
    // Start is called before the first frame update
    void Start()
    {
        chineseLocale = LocaleStringParser.ParseLocaleFromTextAsset(textChineseFile);
        englishLocale = LocaleStringParser.ParseLocaleFromTextAsset(textEnglishFile);
        japaneseLocale = LocaleStringParser.ParseLocaleFromTextAsset(textJapaneseFile);
        chichewaLocale = LocaleStringParser.ParseLocaleFromTextAsset(textChichewaFile);
    }

    public string GetLocaleWord(string key)
    {
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                return chineseLocale[key];
            case Locales.English:
                return englishLocale[key];
            case Locales.Japanese:
                return japaneseLocale[key];
            case Locales.Chichewa:
                return chichewaLocale[key];
        }
        return key;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
