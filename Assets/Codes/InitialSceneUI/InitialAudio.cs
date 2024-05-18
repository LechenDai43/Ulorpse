using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InitialAudio : MonoBehaviour
{
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        
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
