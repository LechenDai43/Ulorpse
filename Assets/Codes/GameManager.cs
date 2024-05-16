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
    public bool localChangedFlag;
    #endregion

    #region getter and setter
    public static Locales GetLocale()
    {
        return _instance.locale;
    }

    public static void SetLocale(Locales _locale)
    {
        if (_locale != _instance.locale)
        {
            _instance.localChangedFlag = true;
        }
        _instance.locale = _locale;
    }

    public static bool GetLocaleFlag()
    {
        return _instance.localChangedFlag;
    }
    #endregion


    void Awake()
    {
        Debug.Log("GameManager Awake() called.");
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
        _instance.localChangedFlag = false;
    }

    
}
