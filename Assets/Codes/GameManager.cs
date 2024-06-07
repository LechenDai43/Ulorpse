using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using System;

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
    public int dialogueVolume = 100;
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

    public static int GetDialogueVolume()
    {
        return _instance.dialogueVolume;
    }
    public static void SetDialogueVolume(int _volume)
    {
        _instance.dialogueVolume = _volume;
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
    
    public static string GetFirstName()
    {
        return _instance.Character.FirstName;
    }
    public static void SetFirstName(string _name)
    {
        _instance.Character.FirstName = _name;
    }
    
    public static string GetLastName()
    {
        return _instance.Character.LastName;
    }
    public static void SetLastName(string _name)
    {
        _instance.Character.LastName = _name;
    }
    
    public static Sprite GetCharacterPortrait()
    {
        return _instance.Character.CharacterPortrait;
    }
    public static void SetCharacterPortrait(Sprite _sprite)
    {
        _instance.Character.CharacterPortrait = _sprite;
    }

    public static Exilir[] GetExilirs() {
        Array.Sort(_instance.Character.Exilirs, new ExilirComparer());
        return _instance.Character.Exilirs;
    }
    public static void AddExilir(Exilir _exilir) {
        int newLength = _instance.Character.Exilirs == null? 1: _instance.Character.Exilirs.Length + 1;
        Array.Resize(ref _instance.Character.Exilirs, newLength);
        _instance.Character.Exilirs[newLength - 1] = _exilir;
    }

    public static bool IsExilirEquipped(Exilir _exilir, Elements _element) {
        if (_exilir.Element == _element) {
            if (_exilir.ID == _instance.Character.MetalExilirID) {
                return true;
            }
            else if (_exilir.ID == _instance.Character.MetalExilirID) {
                return true;
            }
            else if (_exilir.ID == _instance.Character.MetalExilirID) {
                return true;
            }
            else if (_exilir.ID == _instance.Character.MetalExilirID) {
                return true;
            }
            else if (_exilir.ID == _instance.Character.MetalExilirID) {
                return true;
            }
            return false;
        }
        else {
            if (_exilir.Element != Elements.Normal) {
                return false;
            }
            switch (_element) {
                case Elements.Metal:
                    return _exilir.ID == _instance.Character.MetalExilirID;
                case Elements.Wood:
                    return _exilir.ID == _instance.Character.WoodExilirID;
                case Elements.Water:
                    return _exilir.ID == _instance.Character.WaterExilirID;
                case Elements.Fire:
                    return _exilir.ID == _instance.Character.FireExilirID;
                case Elements.Earth:
                    return _exilir.ID == _instance.Character.EarthExilirID;
            }
            return false;
        }
    }

    public static void EquipExilir (Exilir _exilir, Elements _element) {        
            switch (_element) {
                case Elements.Metal:
                    _instance.Character.MetalExilirID = _exilir.ID;
                    break;
                case Elements.Wood:
                    _instance.Character.WoodExilirID = _exilir.ID;
                    break;
                case Elements.Water:
                    _instance.Character.WaterExilirID = _exilir.ID;
                    break;
                case Elements.Fire:
                    _instance.Character.FireExilirID = _exilir.ID;
                    break;
                case Elements.Earth:
                    _instance.Character.EarthExilirID = _exilir.ID;
                    break;
            }
    }
    #endregion

    #region Pseudo Character
    public Sprite player;
    #endregion
    void Awake()
    {
        if (_instance == null)
        {
            Character = new Saving();
            _instance = this;
            DontDestroyOnLoad(gameObject);

            #if UNITY_EDITOR
                this.Character = new Saving();
                Character.LastName = "Tao";
                Character.FirstName = "Tianshuo";
                Character.Gender = Genders.Male;
                Character.Province = Provinces.Tranquil;
                Character.CharacterPortrait = player;
                Character.Exilirs = new Exilir[0];
            #endif
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

    public static void Save() {
        string SaveFilePath;
        if (_instance.Character._FileName != null) {
            SaveFilePath = Path.Combine(Application.persistentDataPath, "SaveFolder", _instance.Character._FileName);
        }
        else if (_instance.Character.LastName != null || _instance.Character.FirstName != null){
            string FileName = _instance.Character.LastName + _instance.Character.FirstName;
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "SaveFolder"))){
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "SaveFolder"));
            }
            string[] files = Directory.GetFiles(Path.Combine(Application.persistentDataPath, "SaveFolder"));
            bool duplicated = false;
            int count = 0;
            do {
                duplicated = false;
                foreach (string file in files) {
                    if (file.EndsWith(FileName + (count == 0? "": count) + ".json")) {
                        duplicated = true;
                        count += 1;
                    }
                }
            }
            while (duplicated);
            SaveFilePath = Path.Combine(Application.persistentDataPath, "SaveFolder", FileName + (count == 0? "": count) + ".json");
            _instance.Character._FileName = FileName + (count == 0? "": count) + ".json";
        }
        else {
            return;
        }
        string JsonData = JsonUtility.ToJson(_instance.Character);
        File.WriteAllText(SaveFilePath, JsonData);
    }
}
