using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Turn { p1Start, p1Draw, p1Pip, p1Play, p1Attack, p1End, p2Start, p2Draw, p2Pip, p2Play, p2Attack, p2End }
public enum WhichPlayer { one = 1, two = 2 }

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public ScriptableObject playerOnePrefab;
	public ScriptableObject playerTwoPrefab;

	public PlayerInfo playerOne;
	public PlayerInfo playerTwo;
	
	public delegate void CardEvent(InSceneCard card);
	public delegate void PlayerEvent(WhichPlayer player);

	public PlayerEvent onStartOfTurn;
	public PlayerEvent onDrawCard;
	public PlayerEvent onAddPip;	
	public CardEvent onSpiritPlay;
	public CardEvent onSpiritFade;
	public CardEvent onAttack;
	public CardEvent onDealDamage;
	public PlayerEvent onEndOfTurn;

	public bool canPlay = false;
	public CardType currAttack;

	[SerializeField]
	private Turn currTurn;
	private FieldHandler p1Field;

	void Start () 
	{
		if(instance != null)
			Destroy(this.gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);

		playerOne = UnityEngine.Object.Instantiate(playerOnePrefab) as PlayerInfo;
		playerTwo = UnityEngine.Object.Instantiate(playerTwoPrefab) as PlayerInfo;

		SetupEvents();

		currTurn = 0;
		currAttack = CardType.Hit;
		p1Field = GameObject.FindGameObjectWithTag("Field").GetComponent<FieldHandler>();

		StartCoroutine(runGame());
	}

	void SetupEvents()
	{
		onStartOfTurn = Debug1;
		onDrawCard = Debug2;
		onAddPip = Debug3;
		onSpiritPlay = Debug4;
		onSpiritFade = Debug5;
		onAttack = Debug6;
		onDealDamage = Debug7;
		onEndOfTurn = Debug8;
	}

	public void advanceTurn()
	{
		//Debug.Log("advance turn from " + currTurn + " to " + (currTurn+1));
		currTurn++;
		if(currTurn > Turn.p2Attack)
			currTurn = Turn.p1Pip;
	}

	IEnumerator runGame()
	{
		while(true)
		{
			/* public enum Turn { p1Draw, p1Pip, p1Play, p1Attack, p2Draw, p2Pip, p2Play, p2Attack } */

			//P1 start of turn
				Debug.Log("start? "+currTurn.ToString());
				onStartOfTurn(WhichPlayer.one);
				advanceTurn();
			//P1 draw
				Debug.Log("draw? "+currTurn.ToString());
				DrawCards(WhichPlayer.one);
				advanceTurn();
			//P1 gain pip
				Debug.Log("pip? "+currTurn.ToString());
				AddPips(WhichPlayer.one);
				advanceTurn();
			//P1 play
				Debug.Log("paly? "+currTurn.ToString());
				canPlay = true;
				Debug.Log("Waiting for plays");
				yield return new WaitUntil( () => currTurn != Turn.p1Play);
				canPlay = false;
			//P1 attack
				Debug.Log(currTurn.ToString());
				Debug.Log("Attacking");
				int damage = p1Field.Attack(currAttack) + playerOne.baseDamage;
				Debug.Log("Player 2 takes " + damage);
				playerTwo.currentHealth -= damage;
				advanceTurn();
			//P1 end of turn
				Debug.Log(currTurn.ToString());
				onEndOfTurn(WhichPlayer.one);
				advanceTurn();
				currTurn = 0;
			
			/*

			//P2 start of turn
			onStartOfTurn(WhichPlayer.two);
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
			onEndOfTurn(WhichPlayer.two);
		
			*/
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
		onAddPip(p);
	}

	void DrawCards(WhichPlayer p, int number = 1)
	{
		if(p == WhichPlayer.one)
			Debug.Log("PlayerONe Draw" + number);
		else
			Debug.Log("PlayerTwo Draw" + number);
		onDrawCard(p);

	}

	void GameOver()
	{
		Debug.Log("GameOver");
	}

	void Debug1(WhichPlayer p){ Debug.Log("GameManger Trigger StartOfTurn"); }
	void Debug2(WhichPlayer p){ Debug.Log("GameManger Trigger Draw Card"); }
	void Debug3(WhichPlayer p){ Debug.Log("GameManger Trigger Add pip"); }
	void Debug4(InSceneCard card){ Debug.Log("GameMAnger Trigger spritplay"); }
	void Debug5(InSceneCard card){ Debug.Log("GameMAnger Trigger spritfade"); }
	void Debug6(InSceneCard card){ Debug.Log("GameMAnger Trigger attack"); }
	void Debug7(InSceneCard card){ Debug.Log("GameMAnger Trigger deal damage"); }
	void Debug8(WhichPlayer p){ Debug.Log("GameManger Trigger eot"); }

	
	public void SetCurrentAttackType(Scrollbar slider)
	{
		if(slider.value*3 == 3)
			currAttack = ((CardType)2);
		else
			currAttack = ((CardType)(slider.value*3));
	}
}
