using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour, DragArea 
{
	public int deckSize;
	public DeckList deckList;
	public GameObject emptyCardPrefab;
	public Vector3 offScreen;

	public Queue<GameObject> cards;

	private GameObject cardHolder;

	IEnumerator Start ()
	{
		deckSize = deckList.deckList.Count;
		cards = new Queue<GameObject>();
		cardHolder = new GameObject();
		cardHolder.name = "CardHolder";
		yield return new WaitUntil(() => deckList != null);
		foreach(var cardInfo in deckList.deckList)
		{
			GameObject temp = Instantiate(emptyCardPrefab, offScreen, Quaternion.identity, cardHolder.transform);
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
		{
			GameManager.instance.GameOver();
			return new GameObject();
		}
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
			{
				GameManager.instance.GameOver();
				card = new GameObject();
			}
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
