﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Turn { p1Start, p1Draw, p1Pip, p1Play, p1Attack, p1End, p2Start, p2Draw, p2Pip, p2Play, p2Attack, p2End }
public enum WhichPlayer { one = 1, two = 2 }

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public LayerMask cardLayer;

	public PlayerInfo playerOne;
	public PlayerInfo playerTwo;

	public GameObject inScenePlayer1;
	public GameObject inScenePlayer2;
	public GameObject inSceneDeckPlayer1;
	public GameObject inSceneDeckPlayer2;
	public GameObject inSceneHandPlayer1;
	public GameObject inSceneHandPlayer2;	
	public GameObject inSceneFieldPlayer1;
	public GameObject inSceneFieldPlayer2;
	
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

	public bool canPlay;
	public CardType currAttack;

	public Queue<TriggeredAbility> toTarget;

	[SerializeField]
	private Turn currTurn;

	void Start () 
	{
		if(instance != null)
			Destroy(this.gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);

		playerOne = UnityEngine.Object.Instantiate(playerOne);
		playerTwo = UnityEngine.Object.Instantiate(playerTwo);
		playerOne.inScenePlayer = inScenePlayer1.GetComponent<InScenePlayer>();
		playerTwo.inScenePlayer = inScenePlayer2.GetComponent<InScenePlayer>();
		playerOne.playerDeck = inSceneDeckPlayer1.GetComponent<DeckHandler>();
		playerOne.playerDeck.setController(playerOne);
		playerTwo.playerDeck = inSceneDeckPlayer2.GetComponent<DeckHandler>();
		playerOne.playerHand = inSceneHandPlayer1.GetComponent<HandHandler>();
		playerTwo.playerHand = inSceneHandPlayer2.GetComponent<HandHandler>();
		playerOne.playerField = inSceneFieldPlayer1.GetComponent<FieldHandler>();
		playerTwo.playerField = inSceneFieldPlayer2.GetComponent<FieldHandler>();

		SetupEvents();

		DrawCards(WhichPlayer.one, playerOne.playerHand.maxHandSize - 1);

		currTurn = 0;
		currAttack = CardType.Hit;
		canPlay = false;
		toTarget = new Queue<TriggeredAbility>();

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

	public void advanceTurnPublic()
	{
		StartCoroutine(advanceTurn());
	}

	private IEnumerator advanceTurn()
	{
		Debug.Log("advance turn from " + currTurn + " to " + (currTurn+1));
		yield return new WaitUntil( () => toTarget.Count == 0);
		currTurn++;
		if(currTurn > Turn.p2End)
			currTurn = Turn.p1Pip;
	}

	public async Task<GameObject> pickTarget(TriggeredAbility a)
	{
		toTarget.Enqueue(a);
		while(toTarget.Peek().ID != a.ID)
		{
			await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
		}

		GameObject temp = null;
		//int counter = 0;
		while(temp == null)
		{
//			Debug.Log("iswaiting for target " + counter++);
			if(Input.GetMouseButtonDown(0))
			{
				//Debug.Log("click");
				Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(clickPos.x, clickPos.y, -10));
				//Debug.Log(mousePosition);
				//Debug.DrawLine(mousePosition, mousePosition+Vector3.forward*100, Color.red, 3);
				RaycastHit2D ray = Physics2D.Raycast(mousePosition, Vector3.forward, 100, cardLayer);
				if(ray.collider != null)
					temp = ray.collider.gameObject;
			}
			await Task.Delay(1);//TimeSpan.FromSeconds(Time.deltaTime));
		}
		toTarget.Dequeue();

		return temp;
	}

	public Turn getCurrentTurn()
	{
		return currTurn;
	}

	IEnumerator runGame()
	{
		int damage;
		while(true)
		{
			// public enum Turn { p1Draw, p1Pip, p1Play, p1Attack, p2Draw, p2Pip, p2Play, p2Attack } 

			//P1 start of turn
				//Debug.Log("start? "+currTurn.ToString());
				onStartOfTurn(WhichPlayer.one);
				yield return StartCoroutine(advanceTurn());
			//P1 draw
				Debug.Log("draw? "+currTurn.ToString());
				DrawCards(WhichPlayer.one);
				yield return StartCoroutine(advanceTurn());
			//P1 gain pip
				Debug.Log("pip? "+currTurn.ToString());
				AddPips(WhichPlayer.one);
				yield return StartCoroutine(advanceTurn());
			//P1 play
				Debug.Log("play? "+currTurn.ToString());
				canPlay = true;
				Debug.Log("Waiting for plays");
				yield return new WaitUntil( () => currTurn != Turn.p1Play);
				canPlay = false;
			//P1 attack
				Debug.Log(currTurn.ToString());
				//Debug.Log("Attacking");
				damage = playerOne.playerField.Attack(currAttack) + playerOne.baseDamage;
				damage = (int)Mathf.Ceil(damage * playerTwo.resists[(int)currAttack, (int)playerOne.lastAttack]);
				playerOne.lastAttack = currAttack;
				playerTwo.currentHealth -= damage;
				yield return StartCoroutine(advanceTurn());
			//P1 end of turn
				//Debug.Log(currTurn.ToString());
				onEndOfTurn(WhichPlayer.one);
				yield return StartCoroutine(advanceTurn());
			

			//P2 start of turn
				//Debug.Log("start? "+currTurn.ToString());
				onStartOfTurn(WhichPlayer.two);
				yield return StartCoroutine(advanceTurn());
			//P2 draw
				Debug.Log("draw? "+currTurn.ToString());
				DrawCards(WhichPlayer.two);
				yield return StartCoroutine(advanceTurn());
			//P2 gain pip
				Debug.Log("pip? "+currTurn.ToString());
				AddPips(WhichPlayer.two);
				yield return StartCoroutine(advanceTurn());
			//P2 play
				Debug.Log("play? "+currTurn.ToString());
				canPlay = true;
				Debug.Log("Waiting for plays");
				yield return new WaitUntil( () => currTurn != Turn.p2Play);
				canPlay = false;
			//P2 attack
				Debug.Log(currTurn.ToString());
				//Debug.Log("Attacking");
				damage = playerTwo.playerField.Attack(currAttack) + playerTwo.baseDamage;
				damage = (int)Mathf.Ceil(damage * playerOne.resists[(int)currAttack, (int)playerOne.lastAttack]);
				playerTwo.lastAttack = currAttack;
				playerOne.currentHealth -= damage;
				yield return StartCoroutine(advanceTurn());
			//P2 end of turn
				//Debug.Log(currTurn.ToString());
				onEndOfTurn(WhichPlayer.two);
				yield return StartCoroutine(advanceTurn());
		}
		
	}

	void Update()
	{
		UpdateDisplay();
		if(playerOne.currentHealth <= 0 || playerTwo.currentHealth <= 0)
			GameOver();
	}

	void UpdateDisplay()
	{
		InScenePlayer temp = playerOne.inScenePlayer.GetComponent<InScenePlayer>();
		temp.healthText.text = playerOne.currentHealth.ToString();
		temp.pipText.text = playerOne.pips.ToString();
		temp.nameText.text = playerOne.playerName;
		temp = playerTwo.inScenePlayer.GetComponent<InScenePlayer>();
		temp.healthText.text = playerTwo.currentHealth.ToString();
		temp.pipText.text = playerTwo.pips.ToString();
		temp.nameText.text = playerTwo.playerName;
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
		{
			if(number == 1)
				playerOne.playerHand.addCard(playerOne.playerDeck.drawCards());
			else
				foreach(var card in playerOne.playerDeck.drawCards(number))
				{
					playerOne.playerHand.addCard(card);
				}	
			//Debug.Log("PlayerOne Draw" + number);
		}
		else
			//Debug.Log("PlayerTwo Draw" + number);
		onDrawCard(p);

	}

	public void GameOver()
	{
		Debug.Log("GameOver");
		StopAllCoroutines();
		gameObject.SetActive(false);
		//Application.Quit();
	}

	// void Debug1(WhichPlayer p){ }
	// void Debug2(WhichPlayer p){ }
	// void Debug3(WhichPlayer p){ }
	// void Debug4(InSceneCard card){ }
	// void Debug5(InSceneCard card){ }
	// void Debug6(InSceneCard card){ }
	// void Debug7(InSceneCard card){ }
	// void Debug8(WhichPlayer p){ }
	void Debug1(WhichPlayer p){ Debug.Log("GameManger Trigger StartOfTurn"); }
	void Debug2(WhichPlayer p){ Debug.Log("GameManger Trigger DrawCard"); }
	void Debug3(WhichPlayer p){ Debug.Log("GameManger Trigger AddPip"); }
	void Debug4(InSceneCard card){ Debug.Log("GameManger Trigger SpritPlay"); }
	void Debug5(InSceneCard card){ Debug.Log("GameManger Trigger SpritFade"); }
	void Debug6(InSceneCard card){ Debug.Log("GameManger Trigger Atack"); }
	void Debug7(InSceneCard card){ Debug.Log("GameManger Trigger DealDamage"); }
	void Debug8(WhichPlayer p){ Debug.Log("GameManger Trigger EndOfTurn"); }

	
	public void SetCurrentAttackType(Scrollbar slider)
	{
		if(slider.value*3 == 3)
			currAttack = ((CardType)2);
		else
			currAttack = ((CardType)(slider.value*3));
	}
}
