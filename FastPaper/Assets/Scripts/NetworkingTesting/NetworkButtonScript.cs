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

	public void StartGame()
	{
		//if(NetworkedPlayer.numInstPlayers == 2)
			NetworkManager.singleton.ServerChangeScene("NetworkingIngameTestScene");
		GameManager.instance.playerOne.playerDeck.deckList = DeckManager.instance.playerOneDeck;
		GameManager.instance.playerTwo.playerDeck.deckList = DeckManager.instance.playerTwoDeck;
	}

	public void setDeckOne(int p) { DeckManager.instance.setDeck(p, 1); }
	public void setDeckTwo(int p) { DeckManager.instance.setDeck(p, 2); }
	public void setDeckThree(int p) { DeckManager.instance.setDeck(p, 3); }
	public void setDeckFour(int p) { DeckManager.instance.setDeck(p, 4); }

	//private void 
}
