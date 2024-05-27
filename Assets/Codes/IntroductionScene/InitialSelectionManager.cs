using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InitialSelectionManager : MonoBehaviour
{
    
    public TextAsset MapLabelFileCN, MapLabelFileEN, MapLabelFileJP;
    public Dictionary<string, string> MapLabels;
    public TextAsset SelectionLinesFileCN, SelectionLinesFileEN, SelectionLinesFileJP;
    public Dictionary<string, string> SelectionLines;
    public TextAsset LineVariable;
    public List<string> LineVariableList = new List<string>();

    public Image MapImage;
    public Text[] Lables;
    public float[] xRatio, yRatio;
    public string[] LabelVariables;
    // Start is called before the first frame update
    void Start()
    {
       switch (GameManager.GetLocale())
       {
            case Locales.Chinese:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileCN);
                break;
            case Locales.English:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileEN);
                break;
            case Locales.Japanese:
                MapLabels = LocaleStringParser.ParseLocaleFromTextAsset(MapLabelFileJP);
                break;
        }
       PositionLables();
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
}
