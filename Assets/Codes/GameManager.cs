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

    public Saving Character;

    #region variables
    public Locales locale;
    public int soundVolume = 100;
    public int musicVolume = 100;
    public bool musicInterruptedOnSceneChanging = false;
    public float musicPausedAt = 0.0F;
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

    public static bool IsMusicInterruptedOnSceneChanging()
    {
        return _instance.musicInterruptedOnSceneChanging;
    }
    public static void SetMusicInterruptedOnSceneChanging(bool _volume)
    {
        _instance.musicInterruptedOnSceneChanging = _volume;
    }

    public static float GetMusicPausedAt()
    {
        return _instance.musicPausedAt;
    }
    public static void SetMusicPausedAt(float _volume)
    {
        _instance.musicPausedAt = _volume;
    }
    #endregion

    #region Getter and Setter for Saving
    public static Provinces GetProvince()
    {
        return _instance.Character.Province;
    }
    public static void SetProvince(Provinces _province)
    {
        _instance.Character.Province = _province;
    }
    
    public static Genders GetGender()
    {
        return _instance.Character.Gender;
    }
    public static void SetGender(Genders _gender)
    {
        _instance.Character.Gender = _gender;
    }
    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            Character = new Saving();
            _instance = this;
            DontDestroyOnLoad(gameObject);
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

    public static void CreateNewSaving () {
        _instance.Character = new Saving();
    }
}
