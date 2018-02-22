using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : ScriptableObject
{
	public string deckName;
	public List<CardScriptable> deckList = new List<CardScriptable>();
}
