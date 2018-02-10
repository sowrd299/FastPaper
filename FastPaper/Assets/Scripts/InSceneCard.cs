using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneCard : MonoBehaviour 
{
	public CardScriptable cardInfo;

	void Start () 
	{
		
	}
	
	void Cast()
	{
		foreach (var item in cardInfo.abilities)
		{
			switch(item.trigger)
			{
				case Triggers.Opener:
				{
					item.effect.OnTrigger(this);	
				} break;
				case Triggers.Fade:
				{
					GameManager.instance.onSpiritFade += item.effect.OnTrigger;
				} break;
				default:
				{

				} break;
			}
		}
	}
}
