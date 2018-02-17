using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CardNetworkManager : NetworkManager
{
	public static CardNetworkManager instance;

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Client Connection!");
	}

	public override void OnServerReady(NetworkConnection conn)
	{
		Debug.Log("Server Ready!");
	}

	public override void OnStartServer()
	{
		Debug.Log("Creating a server!");
	}

	public void connectToLocalHost()
	{
		NetworkManager.singleton.networkPort = 7777;
		NetworkManager.singleton.networkAddress = "localhost";
		NetworkManager.singleton.StartClient();
	}

	public void startServer()
	{
		NetworkManager.singleton.networkPort = 7777;
		NetworkManager.singleton.StartHost();
	}

}
