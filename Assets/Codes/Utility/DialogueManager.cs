using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    private Queue<DialogueEntity> Content;
    private DialogueEntity currentFocus;
    private string currentContent;
    public Text NameText, ContentText;
    public Button ContinueButton;
    public AudioSource VoicePlayer;
    // Start is called before the first frame update
    public TextAsset TextFileCN, TextFileEN, TextFileJP, TextFileNY;
    public Dictionary<string, string> Texts;
    void Start()
    {
        VoicePlayer.volume = GameManager.GetDialogueVolume() / 100.0F;

        
        switch (GameManager.GetLocale())
       {
            case Locales.Chinese:
                Texts = LocaleStringParser.ParseLocaleFromTextAsset(TextFileCN);
                break;
            case Locales.English:
                Texts = LocaleStringParser.ParseLocaleFromTextAsset(TextFileEN);
                break;
            case Locales.Japanese:
                Texts = LocaleStringParser.ParseLocaleFromTextAsset(TextFileJP);
                break;
            case Locales.Chichewa:
                Texts = LocaleStringParser.ParseLocaleFromTextAsset(TextFileNY);
                break;
        }

        ContinueButton.gameObject.GetComponentInChildren<Text>().text = Texts["continue"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContent(Queue<DialogueEntity> _content) {
        Content = _content;
    }

    public void ContinueToNextLine() {
        currentContent = "";
        currentFocus = null;
        if (Content.Count == 0) {
            return;
        }

        currentFocus = Content.Dequeue();
        string[] parts = currentFocus.DialogueContent.Split('Å®');
        string name = parts[0];
        currentContent = parts[1];

        NameText.text = name;
        ContentText.text = "";
        dialogueFinished = false;
        dialogueSkipped = false;

        VoicePlayer.resource = currentFocus.Resource;
        VoicePlayer.Play();
        currentFocus.PreDialogueFunction();
        ContinueButton.interactable = false;
        StartCoroutine(TypeText());
        StartCoroutine(AudioCheck());
    }

    
    private bool dialogueFinished = false;
    private bool dialogueSkipped = false;
    IEnumerator TypeText()
    {
        foreach (char c in currentContent)
        {
            ContentText.text += c;

            if (dialogueSkipped) {  
                ContentText.text = currentContent;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        
        dialogueFinished = true;
        if (!VoicePlayer.isPlaying) {
            ContinueButton.interactable = true;

        }
    }

    IEnumerator AudioCheck() {
        while (VoicePlayer.isPlaying) {
            yield return null;
        }

        if (dialogueFinished || dialogueSkipped) {
            ContinueButton.interactable = true;
        }
    }

    public void SkipDialogueLine() {
        dialogueSkipped = true;
        VoicePlayer.Stop();
        ContinueButton.interactable = true;
    }

    public void ContinueButtonClick() {    
        currentFocus.PostDialogueFunction();
    }
}

public delegate void PreDialogue();
public delegate void PostDialogue();

public class DialogueEntity { 
    public string DialogueContent;
    public PreDialogue PreDialogueFunction;
    public PostDialogue PostDialogueFunction;
    public AudioResource Resource;
}