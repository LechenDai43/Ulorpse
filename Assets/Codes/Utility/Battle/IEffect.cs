using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void ApplyEffect(IFightable fighter);
    string GetLocalizedDescription();
}
