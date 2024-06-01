using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Saving 
{
    [SerializeField]
    public Provinces Province;
    [SerializeField]
    public Genders Gender;
    [SerializeField]
    public string FirstName, LastName;
    [SerializeField]
    public int Level;
    [SerializeField]
    public Exilir[] Exilirs;
    
    [SerializeField]
    public string _FileName;

    public Sprite CharacterPortrait;
}

[System.Serializable]
public enum Genders
{
    Male = 1,
    Female = 2,
}

[System.Serializable]
public enum Provinces
{
    Azure = 1,
    Tranquil = 2,
    Indulge = 3,
}