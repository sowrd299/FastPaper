using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public LayerMask positionLayer;
	[SerializeField]private Vector3 offset;
	private cardScript card;
	private BoxCollider2D col;

	void Start () 
	{
		offset = new Vector3();
		card = GetComponent<cardScript>();
		col = GetComponent<BoxCollider2D>();
	}

	public void OnBeginDrag(PointerEventData p)
	{
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
		Vector3 temp = c.ScreenToWorldPoint(new Vector3(clickPos.x, clickPos.y, 0));
		transform.position = temp + offset;
	}

	public void OnEndDrag(PointerEventData p)
	{
		/*
		Vector3 camPos = p.pressEventCamera.gameObject.transform.position;
		Debug.Log(camPos);
		camPos.x += transform.position.x;
		camPos.y += transform.position.y;
		*/
		Vector3 camPos = transform.position;
		Debug.Log(camPos);

		Debug.DrawLine(camPos, Vector2.zero, Color.red, 1f);

		col.enabled = false;
		RaycastHit2D hit = Physics2D.Raycast(camPos, Vector3.forward, positionLayer);
		col.enabled = true;
		if(hit.collider != null)
		{
			Debug.Log("hit");
			Debug.Log(hit.collider.gameObject.name);
			gameObject.transform.SetParent(hit.collider.gameObject.transform);
		}
	}

}