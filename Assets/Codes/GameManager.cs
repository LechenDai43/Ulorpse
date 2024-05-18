using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    #region variables
    public Locales locale;
    public int soundVolume = 100;
    public int musicVolume = 100;
    #endregion

    #region getter and setter
    public static Locales GetLocale()
    {
        return _instance.locale;
    }
    public static void SetLocale(Locales _locale)
    {
        _instance.locale = _locale;
    }

    public static int GetSoundVolume()
    {
        return _instance.soundVolume;
    }
    public static void SetSoundVolume(int _volume)
    {
        _instance.soundVolume = _volume;
    }

    public static int GetMusicVolume()
    {
        return _instance.musicVolume;
    }
    public static void SetMusicVolume(int _volume)
    {
        _instance.musicVolume = _volume;
    }
    #endregion


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance of GameManager exists
        }
    }  

    void Start()
    {
    }

    void Update()
    {
    }

    
}
