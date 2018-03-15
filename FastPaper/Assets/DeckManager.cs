using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	public static DeckManager instance;

	public DeckList deck1;
	public DeckList deck2;
	public DeckList deck3;
	public DeckList deck4;

	public DeckList playerOneDeck;
	public DeckList playerTwoDeck;

	void Start()
	{
		if(instance != null)
			Destroy(gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void setDeck(int player, int num)
	{
		switch(num)
		{
			//case 1: { if(player == 1) playerOneDeck = deck1; else playerTwoDeck = deck1; } break;
			case 2: { if(player == 1) playerOneDeck = deck2; else playerTwoDeck = deck2; } break;
			case 3: { if(player == 1) playerOneDeck = deck3; else playerTwoDeck = deck3; } break;
			case 4: { if(player == 1) playerOneDeck = deck4; else playerTwoDeck = deck4; } break;
			default: { if(player == 1) playerOneDeck = deck1; else playerTwoDeck = deck1; } break;
		}
	}
}
