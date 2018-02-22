using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHandler : MonoBehaviour, DragArea
{
	public int maxHandSize;
	public GameObject handSlotPrefab;
	public float cardSlotWidth;

	private new BoxCollider2D collider2D;
	private GameObject[] handSlots;
	private List<InSceneCard> cardsInHand;
	private int currentFilled;

	void Start () 
	{
		currentFilled = 0;
		handSlots = new GameObject[maxHandSize];
		collider2D = GetComponent<BoxCollider2D>();
		cardsInHand = new List<InSceneCard>();
		UpdateSlots();
	}

	public void UpdateSlots()
	{
		Array.ForEach(handSlots, (val) => { Destroy(val); });

		float lX = collider2D.bounds.ClosestPoint(Vector2.left*100).x;
		float rX = collider2D.bounds.ClosestPoint(Vector2.right*100).x;
		float increment = Mathf.Abs(lX - rX)/maxHandSize;
		Vector3 space = new Vector3(lX + cardSlotWidth/2, this.transform.position.y, -0.1f);

		for(int x = 0; x < maxHandSize; x++)
		{
			handSlots[x] = Instantiate(handSlotPrefab, space, Quaternion.identity, transform);
			Vector3 i = handSlots[x].transform.localScale;
			Vector3 p = transform.localScale;
			handSlots[x].transform.localScale = new Vector3(i.x/p.x, i.y/p.y,i.z/p.z);
			space = new Vector3(space.x + increment, space.y, space.z);
		}
		
		currentFilled = 0;
		for(int x = 0; x < cardsInHand.Count; x++)
		{
			addCard(cardsInHand[x].gameObject);
		}
	}

	public void addCard(GameObject toAdd)
	{
		if(toAdd.GetComponent<InSceneCard>() == null || currentFilled >= maxHandSize)
			return;
		InSceneCard card = toAdd.GetComponent<InSceneCard>();
		cardsInHand.Add(card);

		HashSet<InSceneCard> temp = new HashSet<InSceneCard>(cardsInHand);
		cardsInHand = new List<InSceneCard>(temp); //removing duplicates

		toAdd.transform.SetParent(this.transform);
		Vector3 currSlot = handSlots[currentFilled++].transform.position;
		toAdd.transform.position = new Vector3(currSlot.x, currSlot.y, -0.2f);
	}

	public void removeCard(GameObject toRemove)
	{
		cardsInHand.Remove(toRemove.GetComponent<InSceneCard>());
		UpdateSlots();
	}
	
	public List<InSceneCard> getCards()
	{
		return cardsInHand;
	}
}
