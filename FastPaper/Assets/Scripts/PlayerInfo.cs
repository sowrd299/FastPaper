using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Player", menuName = "Player")]
[SerializeField]
public class PlayerInfo : ScriptableObject
{
	public string playerName;
	public GameObject playerDeck;
	public GameObject playerHand;
	public GameObject playerField;
	public int pips;
	public int maxHealth;
	public int currentHealth;
}