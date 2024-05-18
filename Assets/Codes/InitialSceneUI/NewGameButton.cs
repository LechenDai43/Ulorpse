using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    Text LoadGameLabel;
    public BackgroundAudio backgroundAudio;
    void Start()
    {
        LoadGameLabel = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LocaleUpdate(InitialSceneLocale locale)
    {
        string label = locale.GetLocaleWord("NewStart");
        LoadGameLabel.text = label;
    }

    public void StartNewGame()
    {
        GameManager.SetMusicInterruptedOnSceneChanging(true);
        GameManager.SetMusicPausedAt(backgroundAudio.Source.time);
        SceneManager.LoadScene("IntroductionScene");
    }
}
