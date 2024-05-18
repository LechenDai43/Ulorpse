using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class IntroductionManagement : MonoBehaviour
{
    public TextAsset IntroductionLineFileCN, IntroductionLineFileEN, IntroductionLineFileJP;
    public Dictionary<string, string> IntroductionLines;

    public Text[] LinesText = new Text[11];
    private int linePointer = 0;
    private bool dialogueFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                IntroductionLines = LocaleStringParser.ParseLocaleFromTextAsset(IntroductionLineFileCN);
                break;
            case Locales.English:
                IntroductionLines = LocaleStringParser.ParseLocaleFromTextAsset(IntroductionLineFileEN);
                break;
            case Locales.Japanese:
                IntroductionLines = LocaleStringParser.ParseLocaleFromTextAsset(IntroductionLineFileJP);
                break;
        }
        foreach (Text line in LinesText)
        {
            line.text = "";
            line.gameObject.SetActive(false);
        }

        StartCoroutine(ShowLines());
    }

    IEnumerator ShowLines()
    {
        foreach (Text line in LinesText)
        {
            linePointer += 1;
            string variableName = "intro";
            if (linePointer < 10)
            {
                variableName += 0.ToString() + linePointer.ToString();
            }
            else
            {
                variableName += linePointer.ToString();
            }
            line.text = IntroductionLines[variableName];
            line.gameObject.SetActive(true);

            if(linePointer == 11)
            {
                dialogueFinished = true;
            }

            yield return new WaitForSeconds(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
