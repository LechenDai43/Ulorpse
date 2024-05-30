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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContent(Queue<DialogueEntity> _content) {
        Content = _content;
    }

    public bool NextLine() {
        currentContent = "";
        currentFocus = null;
        if (Content.Count == 0) {
            return false;
        }

        currentFocus = Content.Dequeue();
        string[] parts = currentFocus.DialogueContent.Split('Å®');
        string name = parts[0];
        currentContent = parts[1];

        NameText.text = name;
        ContentText.text = "";
        dialogueFinished = false;
        dialogueSkipped = false;
        audioFinished = false;

        currentFocus.PreDialogueFunction();
        StartCoroutine(TypeText());

        return true;
    }

    
    private bool dialogueFinished = false;
    private bool dialogueSkipped = false;
    private bool audioFinished = false;
    IEnumerator TypeText()
    {
        foreach (char c in currentContent)
        {
            ContentText.text += c;

            if (dialogueSkipped) {   
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        dialogueFinished = true;
        currentFocus.PostDialogueFunction();
    }

    public void SkipDialogueLine() {
        dialogueSkipped = true;
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