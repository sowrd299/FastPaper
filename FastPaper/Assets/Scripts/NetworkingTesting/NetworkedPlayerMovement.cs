using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerMovement : NetworkBehaviour
{
	public float movementSpeed;

	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
		if(!isLocalPlayer)
			return;
		rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
	}
}
