using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCamera : NetworkBehaviour 
{
	public bool isPlayerOne;
	
	void Start()
	{
		isPlayerOne = true;
	}

	void Update () 
	{
		if(isPlayerOne)
		{
			this.transform.position = new Vector3(0,0,-10);
		}
		else
		{
			this.transform.position = new Vector3(50,0,-10);
		}
	}

}
