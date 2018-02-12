using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneCard : MonoBehaviour 
{
	public CardScriptable cardInfo;

	void Start () 
	{
		cardInfo  = Object.Instantiate(cardInfo);
	}
	
	void OnDestroy()
	{
		OnDeath();
	}

	public void Cast()
	{
		foreach (var item in cardInfo.abilities)
		{
			switch(item.trigger)
			{
				case Triggers.Opener:
				{
					Debug.Log("opener trigger created");
					item.TriggerAbility(this);
				} break;
				case Triggers.Fade:
				{
					Debug.Log("fade trigger created");
					GameManager.instance.onSpiritFade += item.TriggerAbility;
				} break;
				case Triggers.OnPersonalAttack:
				{
					Debug.Log("onattack trigger created");
					GameManager.instance.onAttack += item.TriggerAbility;
				} break;
				default:
				{

				} break;
			}
		}
	}

	public void OnDeath()
	{
		GameManager.instance.onSpiritFade(this);
		Destroy(gameObject);
	}
}
