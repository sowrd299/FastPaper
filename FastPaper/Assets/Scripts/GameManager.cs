using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Turn { p1Pip, p1Play, p1Attack, p2Pip, p2Play, p2Attack }

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public int Player1MaxHealth = 20;
	public int Player2MaxHealth = 20;

	[HideInInspector]public int Player1Health;
	[HideInInspector]public int Player2Health;
	public int Player1Pip;
	public int Player2Pip;

	public UnityEvent onAddPip;
	public UnityEvent onAttack;
	public UnityEvent onStartOfTurn;
	public UnityEvent onEndOfTurn;

	[SerializeField]private Turn currTurn;

	void Start () 
	{
		if(instance != null)
			Destroy(this.gameObject);
		instance = this;
		DontDestroyOnLoad(gameObject);
		Player1Health = Player1MaxHealth;
		Player2Health = Player2MaxHealth;
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
		if(Player1Health <= 0 || Player2Health <= 0)
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
			Player1Pip++;
			return true;
		}
		else if(whichPlayer == 2)
		{
			Player2Pip++;
			return true;	
		}
		return false;
	}

	void GameOver()
	{
		Debug.Log("GameOver");
	}
}
