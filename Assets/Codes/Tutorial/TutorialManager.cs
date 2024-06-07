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
    public bool paused;
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
            else if (variable.Contains("Line15")) {
                entity.PreDialogueFunction = Line15PreDialogueFunction;
            }
            else if (variable.Contains("Line16")) {
                entity.PreDialogueFunction = Line16PreDialogueFunction;
            }
            else if (variable.Contains("Line17")) {
                entity.PreDialogueFunction = Line17PreDialogueFunction;
            }
            else if (variable.Contains("Line18")) {
                entity.PreDialogueFunction = Line18PreDialogueFunction;
            }
            else if (variable.Contains("Line19")) {
                entity.PreDialogueFunction = Line19PreDialogueFunction;
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
            else if (variable.Contains("Special05")) {
                entity.PostDialogueFunction = Special05PostDialogueFunction;
            }
            else if (variable.Contains("Special06")) {
                entity.PostDialogueFunction = Special06PostDialogueFunction;
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
        if (paused) {
            return;
        }
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
        exilir.ID = 1;

        GameManager.AddExilir(exilir);        

        Player.gameObject.SetActive(false);
        Exilir.gameObject.SetActive(true);
    }    

    private void Line15PreDialogueFunction() {
        NormalPreDialogue();     
        Exilir.gameObject.SetActive(false);
    }

    private void Line16PreDialogueFunction() {
        NormalPreDialogue();     
        Player.gameObject.SetActive(true);
    }

    private void Line17PreDialogueFunction() {
        NormalPreDialogue();     
        Player.gameObject.SetActive(false);
        Opponent.gameObject.SetActive(true);
    }

    private void Line18PreDialogueFunction() {
        NormalPreDialogue();     
        Player.gameObject.SetActive(true);
        Opponent.gameObject.SetActive(false);
    }

    private void Line19PreDialogueFunction() {
        NormalPreDialogue();     
        Player.gameObject.SetActive(false);
        Opponent.gameObject.SetActive(true);
    }

    private void Special02PreDialogueFunction() {
        NormalPreDialogue();
        Barrier.SetActive(true);
    }

    private void Special01PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        paused = true;
        GourdButton.interactable = true;
    }

    private void Special02PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        paused = true;
        
        ExilirListEntity[] Entities = MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().Entities;
        foreach(ExilirListEntity entity in Entities) {
            Button button = entity.gameObject.GetComponentInChildren<Button>();
            button.interactable = true;
            button.onClick.AddListener(OpenExilirEntityToMoveOn);
        }
        
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.interactable = false;
        Barrier.SetActive(false);
    }

    private void Special03PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        paused = true;
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.interactable = true;
        MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.onClick.AddListener(CloseExilirPanelToMoveOn);
        Barrier.SetActive(false);
    }    

    private void Special04PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        paused = true;
        BodyButton.interactable = true;
    }     

    private void Special05PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        TutorialDialogueManager.gameObject.SetActive(false);
        Barrier.SetActive(false);
        Button SelectExilirButton = MultiPurposePanel.GetComponentInChildren<BodyPanelManager>().SelectButton;
        SelectExilirButton.onClick.AddListener(EquipExilirToMoveOn);
        paused = true;
        Button CloseButton = MultiPurposePanel.GetComponentInChildren<BodyPanelManager>().CloseButton;
        CloseButton.onClick.AddListener(CloseBodyPanelToMoveOn);
        CloseButton.interactable = false;
    }

    private void Special06PostDialogueFunction() {
        if (paused) {
            return;
        }
        _dialoguePointer++;
        Barrier.SetActive(false);
        paused = true;
        MultiPurposePanel.GetComponentInChildren<BodyPanelManager>().CloseButton.interactable = true;
    }

    public void OpenGourdToMoveOn() {
        if (_dialoguePointer == 12) {
            TutorialDialogueManager.ContinueToNextLine();
        }
        paused = false;
    }

    public void OpenExilirEntityToMoveOn() {
        if (_dialoguePointer == 13) {
            TutorialDialogueManager.ContinueToNextLine();
            paused = false;
            Barrier.SetActive(true);
            MultiPurposePanel.GetComponentInChildren<ExilirPanelManager>().CloseButton.interactable = false;
        }
    }
    
    public void CloseExilirPanelToMoveOn() {
        if (_dialoguePointer == 14) {
            TutorialDialogueManager.ContinueToNextLine();
            GourdButton.interactable = false;
            BodyButton.interactable = false;
            paused = false;
            MultiPurposePanel.SetActive(false);
        }
    }    

    public void OpenBodyToMoveOn() {
        if (_dialoguePointer == 15) {
            TutorialDialogueManager.ContinueToNextLine();
            paused = false;
            Barrier.SetActive(true);
        }
    }

    public void EquipExilirToMoveOn() {
        if (_dialoguePointer == 19) {
            TutorialDialogueManager.gameObject.SetActive(true);
            TutorialDialogueManager.ContinueToNextLine();
            paused = false;
            Barrier.SetActive(true);
        }
    }
    
    public void CloseBodyPanelToMoveOn() {
        if (_dialoguePointer == 20) {
            TutorialDialogueManager.ContinueToNextLine();
            GourdButton.interactable = false;
            BodyButton.interactable = false;
            paused = false;
            MultiPurposePanel.SetActive(false);
        }
    }   
}
