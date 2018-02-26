using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkedPlayer : NetworkBehaviour
{
	public static int numInstPlayers = 0;
	void Awake() 
	{
		Debug.Log("Called");
		if(isLocalPlayer)
		{
			Debug.Log("islocalplayer");
			numInstPlayers++;
			if(numInstPlayers == 2 && Camera.main != null)
			{
				Camera.main.GetComponent<NetworkCamera>().isPlayerOne = false;
			}
		}
	}
}