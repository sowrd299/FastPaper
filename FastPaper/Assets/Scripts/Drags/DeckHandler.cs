using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour, DragArea 
{
	public int deckSize;
	public DeckList deckList;
	public GameObject emptyCardPrefab;
	public Vector3 offScreen;

	public static int t = 0;

	public Queue<GameObject> cards;

	void Start ()
	{
		deckSize = deckList.deckList.Count;
		cards = new Queue<GameObject>();
		foreach(var cardInfo in deckList.deckList)
		{
			GameObject temp = Instantiate(emptyCardPrefab, offScreen, Quaternion.identity);
			temp.GetComponent<InSceneCard>().cardInfo = cardInfo;
			cards.Enqueue(temp);
		}
		shuffleDeck();
	}

	public void validate()
	{
		Queue<GameObject> temp = new Queue<GameObject>();
		foreach (var item in cards)
		{
			if(item.GetComponent<InSceneCard>() != null)
				temp.Enqueue(item);
			else
				Destroy(item);
		}
		cards = temp;
	}

	public Queue<InSceneCard> getCards()
	{
		validate();
		Queue<InSceneCard> temp = new Queue<InSceneCard>();
		foreach(var item in cards)
			temp.Enqueue(item.GetComponent<InSceneCard>());
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

	public GameObject drawCards()
	{
		if(cards.Count <= 0)
			return new GameObject();
		GameObject temp = cards.Dequeue();
		return temp;
	}

	public void addCard(GameObject toAdd)
	{
		toAdd.transform.position = offScreen;
		toAdd.transform.SetParent(gameObject.transform);
		cards.Enqueue(toAdd);
	} 

	public List<GameObject> drawCards(int numCards)
	{
		List<GameObject> temp = new List<GameObject>();
		for(int x = 0; x < numCards; x++)
		{
			GameObject card;
			if(cards.Count > 0)
				card = cards.Dequeue();
			else
				card = new GameObject();
			temp.Add(card);
		}
		return temp;
	}

	public void setController(PlayerInfo player)
	{
		foreach(var card in getCards())
		{
			card.cardInfo.controllingPlayer = player;
		}
	}
}
