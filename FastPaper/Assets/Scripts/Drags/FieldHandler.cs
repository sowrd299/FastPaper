using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldHandler : MonoBehaviour, DragArea
{
	public GameObject hitArea;
	public GameObject blockArea;
	public GameObject grabArea;

	public List<GameObject> hitCards;
	public List<GameObject> blockCards;
	public List<GameObject> grabCards;

	void Awake()
	{
		hitCards = new List<GameObject>();
		blockCards = new List<GameObject>();
		grabCards = new List<GameObject>();
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
				hitCards.Add(toAdd);
			} break;
			case CardType.Block:
			{
				toUse = blockArea;
				blockCards.Add(toAdd);

			} break;
			case CardType.Grab:
			{
				toUse = grabArea;
				grabCards.Add(toAdd);
			} break;
		}
		
		toAdd.transform.SetParent(toUse.transform);
		toAdd.transform.position = new Vector3(toUse.transform.position.x,toUse.transform.position.y,-0.2f);
	}

}