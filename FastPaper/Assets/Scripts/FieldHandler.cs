using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldHandler : DragArea
{
	public GameObject red;
	public GameObject blue;
	public GameObject green;

	public override void addCard(GameObject toAdd)
	{
		if(toAdd.GetComponent<cardScript>() == null)
			return;
		cardScript card = toAdd.GetComponent<cardScript>();
		GameObject toUse = null;
		switch(card.type)
		{
			case CardType.blue:
			{
				toUse = blue;
			} break;
			case CardType.green:
			{
				toUse = green;
			} break;
			case CardType.red:
			{
				toUse = red;
			} break;
		}
		
		toAdd.transform.SetParent(toUse.transform);
		toAdd.transform.position = new Vector3(toUse.transform.position.x,toUse.transform.position.y,-0.2f);
	}

}