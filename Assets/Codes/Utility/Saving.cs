using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving 
{
    public Provinces Province;
    public Genders Gender;
    public string FirstName, LastName;
    public int Level;
    public Sprite CharacterPortrait;
}

public enum Genders
{
    Male = 1,
    Female = 2,
}

public enum Provinces
{
    Azure = 1,
    Tranquil = 2,
    Indulge = 3,
}