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
	

	public virtual void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
	}

	public void PrimaryStatAdjustment(IFightable fighter) {
		// to apply stat adjustment
	}

	public virtual string GetLocalizedDescription() {
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
		float value = GetValue() * (AdjustType == StatAdjustTypes.Value? 1: 100);
		result += GameManager.NameLocaleManager.ElixirDescriptions[key] + ": +" + ((int)value).ToString() + (AdjustType == StatAdjustTypes.Value? "": "%");
		return result;
	}

	public float GetValue() {
		float baseF, incrementF;
		int rowIndex = AdjustType == StatAdjustTypes.Value? 0: 1;
		switch (StatType) {
			case StatTypes.Attack:
				rowIndex += 2;
				break;
			case StatTypes.Defense:
				rowIndex += 0;				
				break;
			case StatTypes.Health:
				rowIndex += 4;				
				break;
			case StatTypes.Speed:
				rowIndex += 6;				
				break;
		}
		baseF = ExilirNumber.BaseValues[rowIndex,(int)Rarity - 2];
		incrementF = ExilirNumber.IncrementValues[rowIndex,(int)Rarity - 2];

		if (StatType == StatTypes.CriticalDamage) {
			baseF = ExilirNumber.BaseValues[9,(int)Rarity - 2];
			incrementF = ExilirNumber.IncrementValues[9,(int)Rarity - 2];
		} else if (StatType == StatTypes.CriticalRate) {
			baseF = ExilirNumber.BaseValues[8,(int)Rarity - 2];
			incrementF = ExilirNumber.IncrementValues[8,(int)Rarity - 2];
		}

		float result = baseF + (incrementF * (Level - 1));
		return result;
	}
}

[System.Serializable]
public class ElementalElixir: Elixir
{
	[SerializeField]
	public AttackTypes AttackType {get; set;}

	public override void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply elemental adjustment
	}

	public override string GetLocalizedDescription() {
		string result =  BasicLocalizedDescription();
		string key;
		if (AttackType == AttackTypes.BasicAttack) {
			key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[13];
		}
		else {
			key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[14];
		}
		string newLine = GameManager.NameLocaleManager.ElixirDescriptions[key];
		key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[15];
		switch (Element) {
			case Elements.Metal:
				key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[15];
				break;
			case Elements.Wood:
				key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[16];
				break;
			case Elements.Water:
				key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[17];
				break;
			case Elements.Fire:
				key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[18];
				break;
			case Elements.Earth:
				key = GameManager.NameLocaleManager.ElixirDescriptionVariableList[19];
				break;
		}
		newLine = newLine.Replace("{%element%}", GameManager.NameLocaleManager.ElixirDescriptions[key]);
		result += newLine;
		return result;
	}
}

[System.Serializable]
public class DoubleElixir: Elixir
{
	[SerializeField]
	public StatTypes SecondaryStatType {get; set;}
	[SerializeField]
	public StatAdjustTypes SecondaryAdjustType {get; set;}

	
	public override void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply secondary stat adjustment
	}

	public override string GetLocalizedDescription() {
		return BasicLocalizedDescription();
	}
}

[System.Serializable]
public class AttackElixir: Elixir
{
	[SerializeField]
	public AttackModes AttackMode {get; set;}

	public override void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}	

	public override string GetLocalizedDescription() {
		return BasicLocalizedDescription();
	}
}

[System.Serializable]
public class SurvivalElixir: Elixir
{
	[SerializeField]
	public SurvivalSkills SurvivalSkill {get; set;}

	public override void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}

	public override string GetLocalizedDescription() {
		return BasicLocalizedDescription();
	}
}

[System.Serializable]
public class EnhanceElixir: Elixir
{
	[SerializeField]
	public EnhanceSkills EnhanceSkill {get; set;}

	public override void ApplyEffect(IFightable fighter) {
		PrimaryStatAdjustment(fighter);
		
		// to apply attak mode adjustment
	}

	public override string GetLocalizedDescription() {
		return BasicLocalizedDescription();
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

public class ExilirNumber {
	public static float[,] BaseValues = new float[10, 4]
	{
		// Defense Values
		{16f, 26f, 37f, 45f},
		{0.034f, 0.051f, 0.069f, 0.086f},
		// Attack Values
		{21f, 33f, 45f, 56f},
		{0.027f, 0.041f, 0.055f, 0.069f},
		// Health Values
		{39f, 67f, 90f, 112f},
		{0.027f, 0.041f, 0.055f, 0.069f},
		// Speed Values
		{1f, 2f, 3f, 4f},
		{0.034f, 0.051f, 0.069f, 0.086f},
		// Critical Damage Rate Values
		{0.017f, 0.027f, 0.041f, 0.051f},
		{0.044f, 0.062f, 0.082f, 0.103f},
	};
	
	public static float[,] IncrementValues = new float[10, 4]
	{
		// Defense Values
		{4f, 8f, 14f, 16f},
		{0.01f, 0.015f, 0.026f, 0.032f},
		// Attack Values
		{5f, 10f, 17f, 20f},
		{0.009f, 0.012f, 0.021f, 0.033f},
		// Health Values
		{13f, 19f, 34f, 42f},
		{0.009f, 0.012f, 0.021f, 0.033f},
		// Speed Values
		{0.6f, 0.8f, 1.2f, 1.8f},
		{0.01f, 0.015f, 0.026f, 0.041f},
		// Critical Damage Rate Values
		{0.006f, 0.011f, 0.016f, 0.025f},
		{0.013f, 0.018f, 0.032f, 0.05f},
	};

}