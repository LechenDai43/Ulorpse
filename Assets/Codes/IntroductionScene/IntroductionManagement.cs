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
    public TextAsset LineVariable;
    public List<string> LineVariableList = new List<string>();
    public GameObject FirstPane, SecondPane;

    public Text[] LinesText = new Text[11];
    private int linePointer = 0;
    private bool dialogueFinished = false;
    private bool dialogueSkipped = false;

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

        string lineVariableContent = LineVariable.text;
        foreach (string line in lineVariableContent.Split('\n'))
        {
            LineVariableList.Add(line.Trim('\r'));
        }

        StartCoroutine(ShowLines());
    }

    IEnumerator ShowLines()
    {
        foreach (Text line in LinesText)
        {
            string variableName = LineVariableList[linePointer];
            line.text = IntroductionLines[variableName];
            line.gameObject.SetActive(true);

            
            linePointer += 1;
            if(linePointer == 10)
            {
                dialogueFinished = true;
            }

            if (dialogueSkipped) {   
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void SkipDialogue() {
        if (dialogueFinished) {
            FirstPane.SetActive(false);
            SecondPane.SetActive(true);
        }
        else {            
            dialogueSkipped = true;
            _finishAllLines();
        }
    }

    private void _finishAllLines() {
        for (int i = linePointer; i < LinesText.Length; i ++) {
            string variableName = LineVariableList[i];
            Text line = LinesText[i];
            line.text = IntroductionLines[variableName];
            line.gameObject.SetActive(true);
        }
        dialogueFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
