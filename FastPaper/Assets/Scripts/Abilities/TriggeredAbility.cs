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
	public Triggers trigger;
	public PossibleEffects effectName;
	public Effect effect;

	public GameObject target;

	public TriggeredAbility(Triggers t, PossibleEffects e)
	{
		trigger = t;
		effectName = e;
		target = null;
	}

	public async void TriggerAbility(InSceneCard card)
	{
		Debug.Log(1);
		InstantiateAbility();
		Debug.Log(2);
		if(effect.requiresTarget)
		{
			Debug.Log(3);
			GameManager.instance.needsTarget.Enqueue(this);
			Debug.Log(4);
			while(target == null)
				await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
			Debug.Log(target);
			((TargetedEffect)effect).OnTrigger(card, target.GetComponent<InSceneCard>());
		}
		else
			((NonTargetedEffect)effect).OnTrigger(card);
	}



	void InstantiateAbility()
	{
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

	

	public override string ToString()
	{
		string temp = "";
		temp += Enum.GetName(typeof(Triggers), trigger);
		temp += " causes the effect ";
		temp += Enum.GetName(typeof(PossibleEffects), effectName);
		return temp;
	}


}
