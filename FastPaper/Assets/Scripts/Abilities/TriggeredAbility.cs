using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Triggers { Opener = 0, Fade }
public enum PossibleEffects { Draw }

public class TriggeredAbility
{
	private Triggers trigger;
	private PossibleEffects effectName;
	private Effect effect;

	public TriggeredAbility(Triggers t, PossibleEffects e)
	{
		trigger = t;
		effectName = e;
		switch(e)
		{
			case PossibleEffects.Draw:
			{
				effect = new Draw(3);
			} break;
			default:
			{

			} break;
		}
		switch(trigger)
		{
			case Triggers.Opener:
			{
				Debug.Log("Adding event " + e.ToString() + " on trigger " + t.ToString());
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
