using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Player", menuName = "Player")]
[SerializeField]
public class PlayerInfo : ScriptableObject
{
	public string playerName;
	public DeckHandler playerDeck;
	public HandHandler playerHand;
	public FieldHandler playerField;
	public InScenePlayer inScenePlayer;
	
	public int pips;
	public int maxHealth;
	public int currentHealth;
	public int baseDamage;
	public CardType lastAttack;
	//public enum CardType { Hit, Block, Grab };
	//resists [Attack, Defend]
	
	public static readonly float RESISTED = 0.5f;
	public static readonly float UNRESISTED = 1f;
	public static readonly float[,] defaultResists = 
	{//Defending with      H            B           G 
					{ UNRESISTED,   RESISTED, UNRESISTED}, //Attacking with Hit
					{ UNRESISTED, UNRESISTED,   RESISTED}, //Attacking with Block
					{   RESISTED, UNRESISTED, UNRESISTED}  //Attackiong with Grab
	};

	public float[,] resists = PlayerInfo.defaultResists;
}