using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleStringParser
{
    
    public static Dictionary<string, string> ParseLocaleFromTextAsset(TextAsset textAsset)
    {
        Dictionary<string, string> result = new Dictionary<string, string> ();

        string content = textAsset.text;
        foreach (string line in content.Split('|'))
        {
            if (!line.Contains(":"))
            {
                continue;
            }
            string newLine = line.Trim('\r').Trim('\n');
            string[] parts = newLine.Split(':');
            result.Add(parts[0], parts[1]);
        }

        return result;
    }

    public static List<string> LoadLocalVariableNames(TextAsset textAsset) {
        List<string> result = new List<string>();
        string variableContent = textAsset.text;
        foreach (string line in variableContent.Split('\n'))
        {
            result.Add(line.Trim('\r'));
        }
        return result;
    }
}

