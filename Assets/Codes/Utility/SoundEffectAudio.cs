using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectAudio : MonoBehaviour
{
    public AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameManager.Instance;

        // Check if GameManager instance is not null
        if (gameManager != null)
        {
            Source.volume = GameManager.GetSoundVolume() / 100.0F;
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

    public void Play()
    {
        Source.Play();
    }
}
