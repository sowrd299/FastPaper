using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Turn { p1Pip, p1Play, p1Attack, p2Pip, p2Play, p2Attack }

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public ScriptableObject playerOnePrefab;
	public ScriptableObject playerTwoPrefab;

	[HideInInspector]public PlayerInfo playerOne; //= new Player();
	[HideInInspector]public PlayerInfo playerTwo; //= ScriptableObject.CreateInstance<Player>();

	/*public UnityEvent onAddPip;
	public UnityEvent onAttack;
	public UnityEvent onStartOfTurn;
	public UnityEvent onEndOfTurn;*/

	[SerializeField]private Turn currTurn;

	void Start () 
	{
		if(instance != null)
			Destroy(this.gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);

		playerOne = Object.Instantiate(playerOnePrefab) as PlayerInfo;
		playerTwo = Object.Instantiate(playerTwoPrefab) as PlayerInfo;

		currTurn = Turn.p1Pip;
	}

	public void advanceTurn()
	{
		currTurn++;
		if(currTurn > Turn.p2Attack)
			currTurn = Turn.p1Pip;
	}

	void Update()
	{
		if(playerOne.currentHealth <= 0 || playerTwo.currentHealth <= 0)
			GameOver();

		bool toAdvance = false;
		switch(currTurn)
		{
			case Turn.p1Pip:
			{
				toAdvance = AddPips(1);
			} break;
			case Turn.p1Play:
			{
				toAdvance = false;
			} break;
			case Turn.p1Attack:
			{
				toAdvance = true;
			} break;
			case Turn.p2Pip:
			{
				toAdvance = AddPips(2);
			} break;
			case Turn.p2Play:
			{
				toAdvance = false;
			} break;
			case Turn.p2Attack:
			{
				toAdvance = true;
			} break;
		}
		if(toAdvance)
			advanceTurn();
	}

	bool AddPips(int whichPlayer)
	{
		if(whichPlayer == 1)
		{
			playerOne.pips++;
			return true;
		}
		else if(whichPlayer == 2)
		{
			playerTwo.pips++;
			return true;	
		}
		return false;
	}

	void GameOver()
	{
		Debug.Log("GameOver");
	}
}
