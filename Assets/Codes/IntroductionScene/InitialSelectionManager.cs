using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InitialSelectionManager : MonoBehaviour
{
    
    public TextAsset MapLabelFileCN, MapLabelFileEN, MapLabelFileJP, MapLabelFileNY;
    public Dictionary<string, string> MapLabels;
    public TextAsset SelectionLinesFileCN, SelectionLinesFileEN, SelectionLinesFileJP, SelectionLinesFileNY;
    public Dictionary<string, string> SelectionLines;
    public TextAsset LineVariable;
    public List<string> LineVariableList = new List<string>();
    public TextAsset SelectionSceneTextFileCN, SelectionSceneTextFileEN, SelectionSceneTextFileJP, SelectionSceneTextFileNY;
    public Dictionary<string, string> SelectionSceneTexts;

    public Image MapImage;
    public Text[] Lables;
    public float[] xRatio, yRatio;
    public string[] LabelVariables;

    public Text[] DialogueTexts;
    public Button ContinueButton, QingButton, XuButton, YanButton;
    // Start is called before the first frame update
    void Start()
    {
       switch (GameManager.GetLocale())
       {
            case Locales.Chinese:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileCN);
                SelectionLines = LocaleStringParser.ParseLocaleFromTextAsset(SelectionLinesFileCN);
                SelectionSceneTexts = LocaleStringParser.ParseLocaleFromTextAsset(SelectionSceneTextFileCN);
                break;
            case Locales.English:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileEN);
                SelectionLines = LocaleStringParser.ParseLocaleFromTextAsset(SelectionLinesFileEN);
                SelectionSceneTexts = LocaleStringParser.ParseLocaleFromTextAsset(SelectionSceneTextFileEN);
                break;
            case Locales.Japanese:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileJP);
                SelectionLines = LocaleStringParser.ParseLocaleFromTextAsset(SelectionLinesFileJP);
                SelectionSceneTexts = LocaleStringParser.ParseLocaleFromTextAsset(SelectionSceneTextFileJP);
                break;
            case Locales.Chichewa:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileNY);
                SelectionLines = LocaleStringParser.ParseLocaleFromTextAsset(SelectionLinesFileNY);
                SelectionSceneTexts = LocaleStringParser.ParseLocaleFromTextAsset(SelectionSceneTextFileNY);
                break;
        }
        
       LineVariableList = LocaleStringParser.LoadLocalVariableNames(LineVariable);
       ContinueButton.gameObject.GetComponentInChildren<Text>().text = SelectionSceneTexts["ContinueButton"];
       PositionLables();
       PopulateDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void PositionLables() {
        RectTransform imageRect = MapImage.rectTransform;
        float width = imageRect.rect.width,
        height = imageRect.rect.height;

        for (int i = 0; i < Lables.Length; i++) {
            float widthL = xRatio[i] * width;
            float heightL = - yRatio[i] * height;
            Text label = Lables[i];
            label.rectTransform.anchoredPosition = new Vector2(widthL, heightL);
            label.text = MapLabels[LabelVariables[i]];
        }
    }

    private void PopulateDialogue(int _start) {
        for (int i = 0; i < 3; i++) {
            string variableName = LineVariableList[i + _start];
            string textContent = SelectionLines[variableName];
            DialogueTexts[i].text = textContent;
        }
    }

    private bool _nextDialogue;
    public void NextButtonFunction() {
        if (!_nextDialogue){
            PopulateDialogue(3);
            _nextDialogue = true;
        }
        else {
            ContinueButton.gameObject.SetActive(false);

            QingButton.gameObject.SetActive(true);
            QingButton.gameObject.GetComponentInChildren<Text>().text = MapLabels[LabelVariables[0]];

            XuButton.gameObject.SetActive(true);
            XuButton.gameObject.GetComponentInChildren<Text>().text = MapLabels[LabelVariables[1]];

            YanButton.gameObject.SetActive(true);
            YanButton.gameObject.GetComponentInChildren<Text>().text = MapLabels[LabelVariables[2]];
            
            for (int i = 0; i < 3; i++) {
                DialogueTexts[i].text = "";
            }
            string variableName = LineVariableList[6];
            string textContent = SelectionLines[variableName];
            DialogueTexts[0].text = textContent;
        }
    }

    public GameObject ProvinceSelectionPanel, GenderAndNameSettingPanel;
    public void ProvinceSelection(int _P) {
        GameManager.SetProvince((Provinces)_P);
        ProvinceSelectionPanel.SetActive(false);
        GenderAndNameSettingPanel.SetActive(true);
    }
}
