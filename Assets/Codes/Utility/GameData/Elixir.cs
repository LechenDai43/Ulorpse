using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Elixir: IEffect
{
	[SerializeField]
	public string Name {get; set;}
	[SerializeField]
	public string Description {get; set;}
	[SerializeField]
	public Rarities Rarity {get; set;}
	[SerializeField]
	public Elements Element {get; set;}
	[SerializeField]
	public StatTypes StatType {get; set;}
	[SerializeField]
	public StatAdjustTypes AdjustType {get; set;}
	[SerializeField]
	public int Level {get; set;}
	[SerializeField]
	public int ID {get; set;}
	[SerializeField]
    public bool IsSpecialSprite;
	[SerializeField]
    public Sprite SpecialSprite;
	

	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
	}

	public void PrimaryStatAdjustment(IFightable fighter) {
		// to apply stat adjustment
	}

	public string GetLocalizedDescription() {
		return BasicLocalizedDescription();
	}

	public string GetLocalizedName() {
		if (!IsSpecialSprite) {
			string standardName = "";
			int index = ((int)Element - 1) * 4 + (int)Rarity - 2;
			if (GameManager.NameLocaleManager != null) {
				string key = GameManager.NameLocaleManager.CommonElixirNameVariableList[index];
				return GameManager.NameLocaleManager.CommonElixirNames[key];
			}
		}
		return Name;
	}

	public string BasicLocalizedDescription() {
		string result = "";
		string key;
		int index;

		// for Level
		key = "level";
		result += GameManager.NameLocaleManager.ElixirDescriptions[key];
		result += ": " + Level.ToString() + "\r\n";

		// for basic effect
		index = (int)StatType;
		key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[index];
		result += GameManager.NameLocaleManager.ElixirDescriptions[key] + " ";
		index = (int)AdjustType + 6;
		key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[index];
		result += GameManager.NameLocaleManager.ElixirDescriptions[key] + ": " + 1.ToString();
		return result;
	}
}

[System.Serializable]
public class ElementalElixir: Elixir
{
	[SerializeField]
	public AttackTypes AttackType {get; set;}

	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply elemental adjustment
	}
}

[System.Serializable]
public class DoubleElixir: Elixir
{
	[SerializeField]
	public StatTypes SecondaryStatType {get; set;}
	[SerializeField]
	public StatAdjustTypes SecondaryAdjustType {get; set;}

	
	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply secondary stat adjustment
	}
}

[System.Serializable]
public class AttackElixir: Elixir
{
	[SerializeField]
	public AttackModes AttackMode {get; set;}

	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}
}

[System.Serializable]
public class SurvivalElixir: Elixir
{
	[SerializeField]
	public SurvivalSkills SurvivalSkill {get; set;}

	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}
}

[System.Serializable]
public class EnhanceElixir: Elixir
{
	[SerializeField]
	public EnhanceSkills EnhanceSkill {get; set;}

	public void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}
}

[System.Serializable]
public enum Elements {
	Metal = 1,
	Wood = 2,
	Water = 3,
	Fire = 4,
	Earth = 5,
	Normal = 6
}

[System.Serializable]
public enum Rarities {
	Common = 2,
	Rare = 3,
	Epic = 4,
	Legendary = 5
}

[System.Serializable]
public enum StatTypes {
	Attack = 1,
	Defense = 2,
	Health = 3,
	CriticalDamage = 4,
	CriticalRate = 5,
	Speed = 6
}

[System.Serializable]
public enum StatAdjustTypes {
	Rate = 1,
	Value = 2
}

[System.Serializable]
public enum AttackTypes {
	BasicAttack,
	Skill
}

[System.Serializable]
public enum AttackModes {
	ChargedAttack ,
	BlastAttack

}

[System.Serializable]
public enum SurvivalSkills {
	Survival,
	Defense,
	Restore,
	Dispel
}

[System.Serializable]
public enum EnhanceSkills {
	IncreaseAttack,
	IncreaseDefense,
	DecreaseAttack,
	DecreaseDefense,
	Nullification
}

public class ElixirComparer: IComparer 
{
	int IComparer.Compare(object x, object y) {
		if (x.GetType() != typeof(Elixir) || y.GetType() != typeof(Elixir)) {
			return 0;
		}

		Elixir elixirX = (Elixir)x, elixirY = (Elixir)y;
		if (elixirX.Level != elixirY.Level) {
			return elixirY.Level - elixirX.Level;
		}
		if (elixirX.Rarity == elixirY.Rarity) {			
			return (int)elixirX.Element - (int)elixirY.Element;
		}
		else {
			return (int)elixirY.Rarity - (int)elixirX.Rarity;
		}
	}
}