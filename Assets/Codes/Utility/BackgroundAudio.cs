using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class BackgroundAudio : MonoBehaviour
{
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            Source.volume = (float)GameManager.GetMusicVolume() / 100;
            if (GameManager.IsMusicInterruptedOnSceneChanging())
            {
                Source.time = GameManager.GetMusicPausedAt();
                GameManager.SetMusicInterruptedOnSceneChanging(false);
                GameManager.SetMusicPausedAt(0);
            }
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateVolume(float volume)
    {
        Source.volume = volume;
    }
}
