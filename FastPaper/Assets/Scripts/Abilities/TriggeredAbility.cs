using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Triggers { Opener = 0, Fade, OnPersonalAttack }
[System.Serializable]
public enum PossibleEffects { Draw }
[System.Serializable]
public class TriggeredAbility
{
	public Triggers trigger;
	public PossibleEffects effectName;
	public Effect effect;

	public TriggeredAbility(Triggers t, PossibleEffects e)
	{
		trigger = t;
		effectName = e;
	}

	public void TriggerAbility(InSceneCard card)
	{
		InstantiateAbility();
		effect.OnTrigger(card);
	}

	void InstantiateAbility()
	{
		switch(effectName)
		{
			case PossibleEffects.Draw:
			{
				effect = new Draw(1);
			} break;
			default:
			{

			} break;
		}
	}

	

	public override string ToString()
	{
		string temp = "";
		temp += Enum.GetName(typeof(Triggers), trigger);
		temp += " causes the effect ";
		temp += Enum.GetName(typeof(PossibleEffects), effectName);
		return temp;
	}


}
