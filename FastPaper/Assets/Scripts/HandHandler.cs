using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHandler : MonoBehaviour, DragArea
{
	public int handSize;
	public GameObject handSlotPrefab;
	public float cardSlotWidth;

	private BoxCollider2D collider2D;
	private GameObject[] handSlots;
	private List<cardScript> cardsInHand;
	private int currentIndex;

	void Start () 
	{
		currentIndex = 0;
		handSlots = new GameObject[handSize];
		collider2D = GetComponent<BoxCollider2D>();
		cardsInHand = new List<cardScript>();
		UpdateSlots();
	}

	public void UpdateSlots()
	{
		Array.ForEach(handSlots, (val) => { Destroy(val); });

		float lX = collider2D.bounds.ClosestPoint(Vector2.left*100).x;
		float rX = collider2D.bounds.ClosestPoint(Vector2.right*100).x;
		float increment = Mathf.Abs(lX - rX)/handSize;
		Vector3 space = new Vector3(lX + cardSlotWidth/2, this.transform.position.y, -0.1f);

		for(int x = 0; x < handSize; x++)
		{
			handSlots[x] = Instantiate(handSlotPrefab, space, Quaternion.identity, transform);
			Vector3 i = handSlots[x].transform.localScale;
			Vector3 p = transform.localScale;
			handSlots[x].transform.localScale = new Vector3(i.x/p.x, i.y/p.y,i.z/p.z);
			space = new Vector3(space.x+increment, space.y, space.z);
		}
	}

	public void addCard(GameObject toAdd)
	{
		if(toAdd.GetComponent<cardScript>() == null)
			return;
		cardScript card = toAdd.GetComponent<cardScript>();
		cardsInHand.Add(card);
		toAdd.transform.SetParent(this.transform);
		Vector3 currSlot = handSlots[currentIndex++].transform.position;
		toAdd.transform.position = new Vector3(currSlot.x, currSlot.y, -0.2f);
	}

	public void removeCard()
	{

	}
	
	public List<cardScript> getCards()
	{
		return cardsInHand;
	}
}
