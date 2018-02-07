using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScriptable : ScriptableObject 
{
	public new string name;
	public string flavorText;
	public string textBox;
	public int attack;
	public int countdown;
	public int cost;
	public Sprite picture;

	public List<TriggeredAbility> abilities = new List<TriggeredAbility>();

	public PlayerInfo controllingPlayer;

}
