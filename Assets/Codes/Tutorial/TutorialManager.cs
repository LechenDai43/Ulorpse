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
    public Button GourdButton, BodyButton;
    public GameObject MultiPurposePanel, Barrier;

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
        GourdButton.interactable = false;
        BodyButton.interactable = false;
        
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
            else if (variable.Contains("Special02")) {
                entity.PreDialogueFunction = Special02PreDialogueFunction;
            }
            else {
                entity.PreDialogueFunction = NormalPreDialogue;
            }

            if (variable.Contains("Special01")) {
                entity.PostDialogueFunction = Special01PostDialogueFunction;
            }
            else if (variable.Contains("Special02")) {
                entity.PostDialogueFunction = Special02PostDialogueFunction;
            }
            else if (variable.Contains("Special03")) {
                entity.PostDialogueFunction = Special03PostDialogueFunction;
            }
            else if (variable.Contains("Special04")) {
                entity.PostDialogueFunction = Special04PostDialogueFunction;
            }
            else {
                entity.PostDialogueFunction = NormalPostDialogue;
            }
            entity.Resource = DubResources[pointer];

            DialogueQueue.Enqueue(entity);
            pointer++;
        }
    }

    private void NormalPostDialogue() {
        Debug.Log(_dialoguePointer);
        _dialoguePointer++;
        TutorialDialogueManager.ContinueToNextLine();
    }

    private void NormalPreDialogue() {

    }

    private void Line07PreDialogueFunction() {
        NormalPreDialogue();

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
        exilir.ID = 0;

        GameManager.AddExilir(exilir);        

        Player.gameObject.SetActive(false);
        Exilir.gameObject.SetActive(true);
    }

    private void Special02PreDialogueFunction() {
        NormalPreDialogue();
        Barrier.SetActive(true);
    }

    private void Special01PostDialogueFunction() {
        _dialoguePointer++;
        TutorialDialogueManager.ContinueButton.interactable = false;
        GourdButton.interactable = true;
        Debug.Log(_dialoguePointer);
    }

    private void Special02PostDialogueFunction() {
        _dialoguePointer++;
        TutorialDialogueManager.ContinueButton.interactable = false;
        
        ExilirListEntity[] Entities = MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().Entities;
        foreach(ExilirListEntity entity in Entities) {
            Button button = entity.gameObject.GetComponentInChildren<Button>();
            button.interactable = true;
            button.onClick.AddListener(OpenExilirEntityToMoveOn);
        }
        Debug.Log(_dialoguePointer);
        Barrier.SetActive(false);
    }

    private void Special03PostDialogueFunction() {
        _dialoguePointer++;
        TutorialDialogueManager.ContinueButton.interactable = false;
        Debug.Log(_dialoguePointer);
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.interactable = true;
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.onClick.AddListener(CloseExilirPanelToMoveOn);
    }    

    private void Special04PostDialogueFunction() {
        _dialoguePointer++;
        TutorialDialogueManager.ContinueButton.interactable = false;
        BodyButton.interactable = true;
        Debug.Log(_dialoguePointer);
    }

    public void OpenGourdToMoveOn() {
        if (_dialoguePointer == 12) {
            TutorialDialogueManager.ContinueToNextLine();
        }
    }

    public void OpenExilirEntityToMoveOn() {
        if (_dialoguePointer == 13) {
            TutorialDialogueManager.ContinueToNextLine();
        }
        Barrier.SetActive(false);
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.interactable = false;
    }
    
    public void CloseExilirPanelToMoveOn() {
        if (_dialoguePointer == 14) {
            TutorialDialogueManager.ContinueToNextLine();
        }
        GourdButton.interactable = false;
        BodyButton.interactable = false;
        MultiPurposePanel.SetActive(false);
    }    

    public void OpenBodyToMoveOn() {
        if (_dialoguePointer == 14) {
            TutorialDialogueManager.ContinueToNextLine();
        }
    }
}
