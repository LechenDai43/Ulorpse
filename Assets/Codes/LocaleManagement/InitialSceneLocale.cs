using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSceneLocale : MonoBehaviour
{
    public TextAsset textChineseFile, textEnglishFile, textJapaneseFile;
    public Dictionary<string, string> chineseLocale, englishLocale, japaneseLocale;
    // Start is called before the first frame update
    void Start()
    {
        chineseLocale = LocaleStringParser.ParseLocaleFromTextAsset(textChineseFile);
        englishLocale = LocaleStringParser.ParseLocaleFromTextAsset(textEnglishFile);
        japaneseLocale = LocaleStringParser.ParseLocaleFromTextAsset(textJapaneseFile);
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
        }
        return key;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
