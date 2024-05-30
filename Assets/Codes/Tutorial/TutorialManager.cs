using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.Audio;

public class TutorialManager : MonoBehaviour
{

    public TextAsset TutorialLineFileCN, TutorialLineFileEN, TutorialLineFileJP, TutorialLineFileNY;
    public Dictionary<string, string> TutorialLines;
    public TextAsset LineVariable;
    public List<string> LineVariableList;
    public List<AudioResource> MaleDubResources;
    public List<AudioResource> FemaleDubResources;
    private List<AudioResource> DubResources;
    public DialogueManager TutorialDialogueManager;

    private int _dialoguePointer = 0;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.GetLocale())
        {
            case Locales.Chinese:
                TutorialLines = LocaleStringParser.ParseLocaleFromTextAsset(TutorialLineFileCN);
                break;
            case Locales.English:
                TutorialLines = LocaleStringParser.ParseLocaleFromTextAsset(TutorialLineFileEN);
                break;
            case Locales.Japanese:
                TutorialLines = LocaleStringParser.ParseLocaleFromTextAsset(TutorialLineFileJP);
                break;
            case Locales.Chichewa:
                TutorialLines = LocaleStringParser.ParseLocaleFromTextAsset(TutorialLineFileNY);
                break;
        }

        if (GameManager.GetGender() == Genders.Male) {
            DubResources = MaleDubResources;
        }
        else {
            DubResources = FemaleDubResources;
        }

        LineVariableList = LocaleStringParser.LoadLocalVariableNames(LineVariable);
        PrepareDialogueQueue();
        TutorialDialogueManager.SetContent(DialogueQueue);
        TutorialDialogueManager.ContinueToNextLine();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Queue<DialogueEntity> DialogueQueue;
    void PrepareDialogueQueue() {
        DialogueQueue = new Queue<DialogueEntity>();
        int pointer = 0;

        foreach (string variable in LineVariableList) {
            DialogueEntity entity = new DialogueEntity();

            string content = TutorialLines[variable].Replace("{%name%}", GameManager.GetFirstName());
            entity.DialogueContent = content;
            if (variable.Contains("Special")) {
                entity.PreDialogueFunction = NormalPreDialogue;
            }
            else {
                entity.PreDialogueFunction = SpecialPreDialogue;
            }
            entity.PostDialogueFunction = NormalPostDialogue;
            entity.Resource = DubResources[pointer];

            DialogueQueue.Enqueue(entity);
            pointer++;
        }
    }

    private void NormalPostDialogue() {
        _dialoguePointer++;
    }

    private void NormalPreDialogue() {

    }

    private void SpecialPreDialogue() {
        NormalPreDialogue();
    }
}
