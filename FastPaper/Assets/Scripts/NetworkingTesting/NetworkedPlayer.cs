using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkedPlayer : NetworkBehaviour
{
	public static int numInstPlayers = 0;
	public bool isPlayerOne;

	void Awake() 
	{
		DontDestroyOnLoad(gameObject);
		Debug.Log("NetworkedPlayer Created");
		numInstPlayers++;
		if(numInstPlayers == 2 && Camera.main != null)
		{
			isPlayerOne = false;
		}
	}
}