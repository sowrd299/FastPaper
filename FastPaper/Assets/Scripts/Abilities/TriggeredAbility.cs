using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public enum Triggers { Opener, Fade, OnPersonalAttack }
[System.Serializable]
public enum PossibleEffects { Draw, CountDownTarget }
[System.Serializable]
public class TriggeredAbility
{
	public static int inst = 0;

	public int ID;
	public Triggers trigger;
	public PossibleEffects effectName;
	public Effect effect;
	public InSceneCard parent;

	public GameObject target;

	public TriggeredAbility(Triggers t, PossibleEffects e)
	{
		trigger = t;
		effectName = e;
		target = null;
	}

	public async void TriggerAbility(InSceneCard card)
	{
		if(!ShouldTrigger(card))
			return;
		Debug.Log("Triggering");
		InstantiateAbility();
		if(effect.requiresTarget)
		{
			target = await GameManager.instance.pickTarget(this);
			Debug.Log(target);
			((TargetedEffect)effect).OnTrigger(card, target.GetComponent<InSceneCard>());
		}
		else
			((NonTargetedEffect)effect).OnTrigger(card);
	}

	private bool ShouldTrigger(InSceneCard card)
	{
		switch(trigger)
		{
			case Triggers.OnPersonalAttack:
			{
				if(card != parent)
					return false;
			} break;
		}
		return true;
	}


	void InstantiateAbility()
	{
		Debug.Log("creating ability with ID " + TriggeredAbility.inst + 1);
		TriggeredAbility.inst++;
		ID = TriggeredAbility.inst;
		switch(effectName)
		{
			case PossibleEffects.Draw:
			{
				effect = new Draw(1);
			} break;
			case PossibleEffects.CountDownTarget:
			{
				effect = new CountDownTarget(1);
			} break;
			default:
			{

			} break;
		}
	}

	public void SetupTrigger(InSceneCard card)
	{
		switch(trigger)
			{
				case Triggers.Opener:
				{
					Debug.Log("opener trigger created");
					TriggerAbility(card);
				} break;
				case Triggers.Fade:
				{
					Debug.Log("fade trigger created");
					GameManager.instance.onSpiritFade += TriggerAbility;
				} break;
				case Triggers.OnPersonalAttack:
				{
					Debug.Log("onattack trigger created");
					GameManager.instance.onAttack += TriggerAbility;
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
