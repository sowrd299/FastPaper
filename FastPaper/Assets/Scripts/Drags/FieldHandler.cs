using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldHandler : MonoBehaviour, DragArea
{
	public GameObject hitArea;
	public GameObject blockArea;
	public GameObject grabArea;

	public List<InSceneCard> hitCards;
	public List<InSceneCard> blockCards;
	public List<InSceneCard> grabCards;

	private float[,] typeEffectiveness = new float[,]
	{ 
		{1f,1f,1f},
		{1f,1f,1f},
		{1f,1f,1f}
	};

	void Awake()
	{
		hitCards = new List<InSceneCard>();
		blockCards = new List<InSceneCard>();
		grabCards = new List<InSceneCard>();
	}

	public void addCard(GameObject toAdd)
	{
		if(toAdd.GetComponent<InSceneCard>() == null)
			return;
		InSceneCard card = toAdd.GetComponent<InSceneCard>();
		GameObject toUse = null;
		switch(card.cardInfo.type)
		{
			case CardType.Hit:
			{
				toUse = hitArea;
				hitCards.Add(card);
			} break;
			case CardType.Block:
			{
				toUse = blockArea;
				blockCards.Add(card);

			} break;
			case CardType.Grab:
			{
				toUse = grabArea;
				grabCards.Add(card);
			} break;
		}
		
		toAdd.transform.SetParent(toUse.transform);
		toAdd.transform.position = new Vector3(toUse.transform.position.x,toUse.transform.position.y,-0.2f);
		Debug.Log("Casting " + card.name);
		GameManager.instance.onSpiritPlay(card);
		card.Cast();

	}

	public int Attack(CardType toAttackWith)
	{
		Debug.Log("ATTACKING with " + toAttackWith.ToString());
		int damage = 0;
		switch(toAttackWith)
		{
			case CardType.Hit:
			{
				Debug.Log("in hit");
				Debug.Log(hitCards.Count);
				foreach(var card in hitCards)
				{
					Debug.Log("HIT: " + card.cardInfo.name + "is attacking with " + card.cardInfo.attack + " attack " + " and " + card.cardInfo.countdown + " countdown remaining.");
					GameManager.instance.onAttack(card);
					damage += card.cardInfo.attack;
					card.cardInfo.countdown--;
					card.UpdateCardInfo();
					if(card.cardInfo.countdown <= 0)
						card.OnDeath();
				}
			} break;
			case CardType.Block:
			{
				foreach(var card in blockCards)
				{
					Debug.Log("BLOCK: " + card.cardInfo.name + "is attacking with " + card.cardInfo.attack + " attack " + " and " + card.cardInfo.countdown + " countdown remaining.");
					GameManager.instance.onAttack(card);
					damage += card.cardInfo.attack;
					card.cardInfo.countdown--;
					card.UpdateCardInfo();
					if(card.cardInfo.countdown <= 0)
						card.OnDeath();
				}

			} break;
			case CardType.Grab:
			{
				foreach(var card in grabCards)
				{
					Debug.Log("GRAB: " + card.cardInfo.name + "is attacking with " + card.cardInfo.attack + " attack " + " and " + card.cardInfo.countdown + " countdown remaining.");
					GameManager.instance.onAttack(card);
					damage += card.cardInfo.attack;
					card.cardInfo.countdown--;
					card.UpdateCardInfo();
					if(card.cardInfo.countdown <= 0)
						card.OnDeath();
				}
			} break;
		}
		return damage;
	}


}