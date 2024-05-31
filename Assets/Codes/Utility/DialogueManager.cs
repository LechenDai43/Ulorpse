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
        VoicePlayer.volume = GameManager.GetDialogueVolume() / 100.0F;
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
            yield return new WaitForSeconds(0.02f);
        }
        
        dialogueFinished = true;
        if (!VoicePlayer.isPlaying) {
            Debug.Log(VoicePlayer.isPlaying);
            ContinueButton.interactable = true;

        }
        currentFocus.PostDialogueFunction();
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
}

public delegate void PreDialogue();
public delegate void PostDialogue();

public class DialogueEntity { 
    public string DialogueContent;
    public PreDialogue PreDialogueFunction;
    public PostDialogue PostDialogueFunction;
    public AudioResource Resource;
}