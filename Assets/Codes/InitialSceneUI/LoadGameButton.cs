using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocaleUpdate()
    {
        if (GameManager.GetLocaleFlag())
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
            }
        }
    }
}
