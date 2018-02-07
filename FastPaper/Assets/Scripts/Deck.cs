using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour, DragArea 
{
	public int deckSize;
	public GameObject tempCard;
	public Vector3 offScreen;

	public static int t = 0;

	public Queue<GameObject> cards;

	void Start ()
	{
		cards = new Queue<GameObject>();
		for(int x = 0; x < deckSize; x++)
		{
			GameObject temp = Instantiate(tempCard, offScreen, Quaternion.identity);
			temp.name = "Card#" + x;
			cards.Enqueue(temp);
		}
		//shuffleDeck();
	}

	public void validate()
	{
		Queue<GameObject> temp = new Queue<GameObject>();
		foreach (var item in cards)
		{
			if(item.GetComponent<cardScript>() != null)
				temp.Enqueue(item);
			else
				Destroy(item);
		}
		cards = temp;
	}

	public Queue<cardScript> getCards()
	{
		validate();
		Queue<cardScript> temp = new Queue<cardScript>();
		foreach(var item in cards)
			temp.Enqueue(item.GetComponent<cardScript>());
		return temp;
	}

	public void shuffleDeck()
	{
		validate();
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

	public void addCard(GameObject toAdd)
	{
		toAdd.transform.position = offScreen;
		toAdd.transform.SetParent(gameObject.transform);
		cards.Enqueue(toAdd);
	} 

	public void drawCards(int numCards)
	{

	}
}
