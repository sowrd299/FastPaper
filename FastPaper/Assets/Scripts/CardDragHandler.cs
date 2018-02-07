using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public LayerMask positionLayer;
	[SerializeField]private Vector3 offset;
	private cardScript card;
	private new BoxCollider2D collider2D;

	void Start () 
	{
		offset = new Vector3();
		card = GetComponent<cardScript>();
		collider2D = GetComponent<BoxCollider2D>();
	}

	public void OnBeginDrag(PointerEventData p)
	{
		transform.position.Set(transform.position.x, transform.position.y , -0.2f);
		Vector2 clickPos = p.position;
		Camera c = p.pressEventCamera;
		Vector3 temp = c.ScreenToWorldPoint(new Vector3(clickPos.x, clickPos.y, 0));
		offset = transform.position - temp;
	}

	public void OnDrag(PointerEventData p)
	{
		card.experience += 2;
		Vector2 clickPos = p.position;
		Camera c = p.pressEventCamera;
		Vector3 temp = c.ScreenToWorldPoint(new Vector3(clickPos.x, clickPos.y, -0.2f));
		transform.position = temp + offset;
	}

	public void OnEndDrag(PointerEventData p)
	{
		Vector3 size = collider2D.bounds.size;
		collider2D.enabled = false;
		RaycastHit2D[] hit = new RaycastHit2D[4];
		/*Debug.Log(transform.position);
		Debug.Log(transform.position + Vector3.right*size.x);
		Debug.Log(transform.position + Vector3.down*size.y+Vector3.right*size.x);
		Debug.Log(transform.position + Vector3.down*size.y);*/
		hit[0] = Physics2D.Raycast(transform.position, Vector3.forward, positionLayer);
		hit[1] = Physics2D.Raycast(transform.position + Vector3.right*size.x, Vector3.forward, positionLayer);
		hit[2] = Physics2D.Raycast(transform.position + Vector3.down*size.y+Vector3.right*size.x, Vector3.forward, positionLayer);
		hit[3] = Physics2D.Raycast(transform.position + Vector3.down*size.y, Vector3.forward, positionLayer);
		collider2D.enabled = true;
		bool done = false;
		Array.ForEach(hit, (ray) =>
		{
			if(!done && ray.collider != null)
			{
				//Debug.Log("hit " + ray.collider.gameObject.name);
				if(ray.collider.gameObject.GetComponent<DragArea>() != null)
				{
					done = true;
					ray.collider.gameObject.GetComponent<DragArea>().addCard(gameObject);
				}
			}
		});
		
	}

}