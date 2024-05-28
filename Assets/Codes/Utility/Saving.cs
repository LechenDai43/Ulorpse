using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving 
{
    public Provinces Province;
    public Genders Gender;
    public string FirstName, LastName;
    public int Level;
}

public enum Genders
{
    Male,
    Female
}

public enum Provinces
{
    Azure,
    Tranquil,
    Indulge
}