using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Draw : NonTargetedEffect 
{
	public int numberOfCards;

	public Draw(int n)
	{
		numberOfCards = n;
		requiresTarget = false;
	}

	public override void OnTrigger(InSceneCard attachedTo)
	{
		Debug.Log("Trigger:Drawing");//attachedTo.cardInfo.controllingPlayer.playerDeck.GetComponent<Deck>().drawCards(numberOfCards);
	}
}
