using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldHandler : MonoBehaviour, DragArea
{
	public GameObject red;
	public GameObject blue;
	public GameObject green;

	public void addCard(GameObject toAdd)
	{
		if(toAdd.GetComponent<InSceneCard>() == null)
			return;
		InSceneCard card = toAdd.GetComponent<InSceneCard>();
		GameObject toUse = null;
		switch(card.cardInfo.type)
		{
			case CardType.hit:
			{
				toUse = blue;
			} break;
			case CardType.block:
			{
				toUse = green;
			} break;
			case CardType.grab:
			{
				toUse = red;
			} break;
		}
		
		toAdd.transform.SetParent(toUse.transform);
		toAdd.transform.position = new Vector3(toUse.transform.position.x,toUse.transform.position.y,-0.2f);
	}

}