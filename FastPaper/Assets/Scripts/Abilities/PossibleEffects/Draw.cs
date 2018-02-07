using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Draw : Effect 
{
	public int numberOfCards;

	public Draw(int n)
	{
		numberOfCards = n;
	}

	public void OnTrigger(CardScriptable attachedTo)
	{
		attachedTo.controllingPlayer.playerDeck.GetComponent<Deck>().drawCards(numberOfCards);
	}
}
