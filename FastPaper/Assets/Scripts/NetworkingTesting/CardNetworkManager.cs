using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CardNetworkManager : NetworkManager
{
	public static CardNetworkManager instance;

	void Awake()
	{
		if(CardNetworkManager.instance != null)
			Destroy(this.gameObject);
		DontDestroyOnLoad(gameObject);
		CardNetworkManager.instance = this;
		NetworkManager.singleton = this;
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Client Connection!" + conn.ToString());
	}

	public override void OnServerReady(NetworkConnection conn)
	{
		Debug.Log("Server Ready!" + conn.ToString());
	}

	public override void OnStartServer()
	{
		Debug.Log("Creating a server!");
	}

	public void connectToLocalHost()
	{
		// NetworkManager.singleton.networkPort = 7777;
		// NetworkManager.singleton.networkAddress = "localhost";
		// NetworkManager.singleton.StartClient();
		CardNetworkManager.instance.networkPort = 7777;
		CardNetworkManager.instance.networkAddress = "localhost";
		CardNetworkManager.instance.StartClient();
	}

	public void startServer()
	{
		// NetworkManager.singleton.networkPort = 7777;
		// NetworkManager.singleton.StartHost();
		CardNetworkManager.instance.networkPort = 7777;
		CardNetworkManager.instance.StartHost();
	}

}
