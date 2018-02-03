using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : DragArea 
{
	public int deckSize;
	public GameObject tempCard;
	public Vector3 offScreen;

	public static int t = 0;

	public Queue<GameObject> cards;

	void Start ()
	{
		cards = new Queue<GameObject>();
		shuffleDeck();
	}

	public void shuffleDeck()
	{
		List<GameObject> temp = new List<GameObject>();
		while(cards.Count != 0)
			temp.Add(cards.Dequeue());
		while(temp.Count != 0)
		{
			GameObject toRemove = temp[Random.Range(0, temp.Count)];
			cards.Enqueue(toRemove);
			temp.Remove(toRemove);
		}
	}

	public GameObject drawCard()
	{
		GameObject temp = cards.Dequeue();
		return temp;
	}

	public override void addCard(GameObject toAdd)
	{
		toAdd.transform.position = offScreen;
		cards.Enqueue(toAdd);
	} 

	void drawCards(int numCards)
	{

	}
}
