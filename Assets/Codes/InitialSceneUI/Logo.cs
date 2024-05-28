using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class Logo : MonoBehaviour
{
    public RawImage image;
    public Texture chineseTexture, englishTexture, japaneseTexture, chichewaTexture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LocaleUpdate()
    {
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                image.texture = chineseTexture;
                break;
            case Locales.English:
                image.texture = englishTexture;
                break;
            case Locales.Japanese:
                image.texture = japaneseTexture;
                break;
            case Locales.Chichewa:
                image.texture = chichewaTexture;
                break;
        }
    }
}
