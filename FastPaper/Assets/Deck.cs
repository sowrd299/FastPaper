using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour 
{
	public int deckSize;
	public GameObject tempCard;

	public static int t = 0;

	public Queue<cardScript> cards;

	void Start ()
	{
		cards = new Queue<cardScript>();
		shuffleDeck();
	}

	public void shuffleDeck()
	{
		List<cardScript> temp = new List<cardScript>();
		while(cards.Count != 0)
			temp.Add(cards.Dequeue());
		while(temp.Count != 0)
		{
			cardScript toRemove = temp[Random.Range(0, temp.Count)];
			cards.Enqueue(toRemove);
			temp.Remove(toRemove);
		}
	}

	public cardScript drawCard()
	{
		cardScript temp = cards.Dequeue();
		return temp;
	}

	public void addCard()
	{
		cards.Enqueue(new cardScript(t++));
	} 

	void drawCards(int numCards)
	{

	}
}
