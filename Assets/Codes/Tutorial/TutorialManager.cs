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
    public Image Player, Exilir, Opponent;
    public Sprite[] Exilirs, Opponents;

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

        Player.sprite = GameManager.GetCharacterPortrait();

        if (GameManager.GetGender() == Genders.Male) {
            DubResources = MaleDubResources;
            switch (GameManager.GetProvince()) {
                case Provinces.Azure:
                    Exilir.sprite = Exilirs[4];
                    Opponent.sprite = Opponents[2];
                    break;
                case Provinces.Tranquil:
                    Exilir.sprite = Exilirs[3];
                    Opponent.sprite = Opponents[0];
                    break;
                case Provinces.Indulge:
                    Exilir.sprite = Exilirs[0];
                    Opponent.sprite = Opponents[1];
                    break;
            }
        }
        else {
            DubResources = FemaleDubResources;
            switch (GameManager.GetProvince()) {
                case Provinces.Azure:
                    Exilir.sprite = Exilirs[2];
                    Opponent.sprite = Opponents[3];
                    break;
                case Provinces.Tranquil:
                    Exilir.sprite = Exilirs[4];
                    Opponent.sprite = Opponents[2];
                    break;
                case Provinces.Indulge:
                    Exilir.sprite = Exilirs[1];
                    Opponent.sprite = Opponents[4];
                    break;
            }
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
            if (variable.Contains("Line07")) {
                entity.PreDialogueFunction = Line07PreDialogueFunction;
            }
            else {
                entity.PreDialogueFunction = NormalPreDialogue;
            }
            entity.PostDialogueFunction = NormalPostDialogue;
            entity.Resource = DubResources[pointer];

            DialogueQueue.Enqueue(entity);
            pointer++;
        }
    }

    private void NormalPostDialogue() {
        _dialoguePointer++;
        TutorialDialogueManager.ContinueToNextLine();
    }

    private void NormalPreDialogue() {

    }

    private void Line07PreDialogueFunction() {
        NormalPreDialogue();

        Player.gameObject.SetActive(false);
        Exilir.gameObject.SetActive(true);

        // Create the initial Exilir
        Exilir exilir = new Exilir();
        if (GameManager.GetGender() == Genders.Male) {
            switch(GameManager.GetProvince()) {
                case Provinces.Azure:
                    exilir.Element = Elements.Earth;
                    break;
                case Provinces.Tranquil:
                    exilir.Element = Elements.Fire;
                    break;
                case Provinces.Indulge:
                    exilir.Element = Elements.Metal;
                    break;
            }
        }
        else {
            switch(GameManager.GetProvince()) {
                case Provinces.Azure:
                    exilir.Element = Elements.Water;
                    break;
                case Provinces.Tranquil:
                    exilir.Element = Elements.Earth;
                    break;
                case Provinces.Indulge:
                    exilir.Element = Elements.Wood;
                    break;
            }
        }
        exilir.Rarity = Rarities.Rare;
        exilir.Name = "An Exilir";
        exilir.Level = 5;
        exilir.StatType = StatTypes.Attack;
        exilir.AdjustType = StatAdjustTypes.Value;
    }
}
