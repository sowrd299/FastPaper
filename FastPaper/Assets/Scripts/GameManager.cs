using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Turn { p1Draw, p1Pip, p1Play, p1Attack, p2Draw, p2Pip, p2Play, p2Attack }
public enum WhichPlayer { one = 1, two = 2 }

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public ScriptableObject playerOnePrefab;
	public ScriptableObject playerTwoPrefab;

	[HideInInspector]public PlayerInfo playerOne;
	[HideInInspector]public PlayerInfo playerTwo;
	
	public PlayerEvent onStartOfTurn;
	public PlayerEvent onDrawCard;
	public PlayerEvent onAddPip;	
	public CardEvent onSpiritPlay;
	public CardEvent onSpiritFade;
	public CardEvent onAttack;
	public CardEvent onDealDamage;
	public PlayerEvent onEndOfTurn;

	public bool canPlay = false;

	[SerializeField]private Turn currTurn;

	void Start () 
	{
		if(instance != null)
			Destroy(this.gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);

		playerOne = UnityEngine.Object.Instantiate(playerOnePrefab) as PlayerInfo;
		playerTwo = UnityEngine.Object.Instantiate(playerTwoPrefab) as PlayerInfo;

		SetupEvents();

		currTurn = Turn.p1Draw;

		StartCoroutine(runGame());
	}

	void SetupEvents()
	{
		onStartOfTurn = new PlayerEvent();
		onDrawCard = new PlayerEvent();
		onAddPip = new PlayerEvent();	
		onSpiritPlay = new CardEvent();
		onSpiritFade = new CardEvent();
		onAttack = new CardEvent();
		onDealDamage = new CardEvent();
		onEndOfTurn = new PlayerEvent();
	}

	public void advanceTurn()
	{
		Debug.Log("advance turn from " + currTurn + " to " + (currTurn+1));
		currTurn++;
		if(currTurn > Turn.p2Attack)
			currTurn = Turn.p1Pip;
	}

	IEnumerator runGame()
	{
		while(true)
		{
			//P1 start of turn
			onStartOfTurn.Invoke(WhichPlayer.one);
			//P1 draw
			DrawCards(WhichPlayer.one);
			advanceTurn();
			//P1 gain pip
			AddPips(WhichPlayer.one);
			advanceTurn();
			//P1 play
			canPlay = true;
			yield return new WaitUntil( () => currTurn != Turn.p1Play);
			canPlay = false;
			//P1 attack
			yield return new WaitUntil( () => currTurn != Turn.p1Attack);
			//P1 end of turn
			onEndOfTurn.Invoke(WhichPlayer.one);
			
			//P2 start of turn
			onStartOfTurn.Invoke(WhichPlayer.two);
			//P2 draw
			DrawCards(WhichPlayer.two);
			advanceTurn();
			//P2 gain pip
			AddPips(WhichPlayer.two);
			advanceTurn();
			//P2 play
			yield return new WaitUntil( () => currTurn != Turn.p2Play);
			//P2 attack
			yield return new WaitUntil( () => currTurn != Turn.p2Attack);
			//P2 end of turn
			onEndOfTurn.Invoke(WhichPlayer.two);
		}
		
	}

	void Update()
	{
		if(playerOne.currentHealth <= 0 || playerTwo.currentHealth <= 0)
			GameOver();
	}

	void AddPips(WhichPlayer p)
	{
		if(p == WhichPlayer.one)
			playerOne.pips++;
		else
			playerTwo.pips++;
		onAddPip.Invoke(p);
	}

	void DrawCards(WhichPlayer p, int number = 1)
	{
		if(p == WhichPlayer.one)
		return;
		onDrawCard.Invoke(p);

	}

	void GameOver()
	{
		Debug.Log("GameOver");
	}
}
