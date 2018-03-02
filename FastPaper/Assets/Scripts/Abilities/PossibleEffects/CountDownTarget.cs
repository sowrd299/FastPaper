using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CountDownTarget : TargetedEffect 
{
	public int amount;

	public CountDownTarget(int a)
	{
		amount = a;
		requiresTarget = true;
	}

	public override void OnTrigger(InSceneCard attachedTo, InSceneCard target)
	{
		Debug.Log("Counting down " + target.cardInfo.name);//attachedTo.cardInfo.controllingPlayer.playerDeck.GetComponent<Deck>().drawCards(numberOfCards);
	}
}