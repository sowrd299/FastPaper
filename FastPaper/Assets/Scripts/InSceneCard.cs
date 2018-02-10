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
					GameManager.instance.onSpiritPlay.AddListener(item.effect.OnTrigger(this));	
				} break;
				case Triggers.Fade:
				{

				} break;
				default:
				{

				} break;
			}
		}
	}
}
