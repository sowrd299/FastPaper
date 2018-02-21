using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkButtonScript : MonoBehaviour
{

	public void returnToLobby()
	{
		Debug.Log("returnToLobby button pressed");
		CardNetworkManager.instance.StopClient();
		CardNetworkManager.instance.StopHost();
	}

	public void startServer()
	{
		Debug.Log("StartServer button pressed");
		CardNetworkManager.instance.startServer();
	}

	public void connectToLocalHost()
	{
		Debug.Log("connectToLocalHost button pressed");
		CardNetworkManager.instance.connectToLocalHost();
	}
}
