using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class SetPlayerLevel : MonoBehaviour
{
    public InputField levelInput, hpInput, attactInput, defenseInput;
    public InputField criticalRateInput, criticalDamageInput;
    // Start is called before the first frame update
    void Start()
    {
        criticalDamageInput.text = "200%";
        criticalRateInput.text = "50%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSetting()
    {
        if (levelInput?.text != null && levelInput.text != "")
        {
            int level = Int32.Parse(levelInput.text);
            hpInput.text = level * 100 + 500 + "";
        }
    }
}
