using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InitialSelectionManager : MonoBehaviour
{
    public Image MapImage;
    public Text[] Lables;
    public float[] xRatio, yRatio;
    // Start is called before the first frame update
    void Start()
    {
       PositionLables();
    }

    // Update is called once per frame
    void Update()
    {
        PositionLables();
    }

    private void PositionLables() {
        RectTransform imageRect = MapImage.rectTransform;
        float width = imageRect.rect.width,
        height = imageRect.rect.height;

        for (int i = 0; i < Lables.Length; i++) {
            float widthL = xRatio[i] * width;
            float heightL = - yRatio[i] * height;
            Text lable = Lables[i];
            lable.rectTransform.anchoredPosition = new Vector2(widthL, heightL);
        }
    }
}
