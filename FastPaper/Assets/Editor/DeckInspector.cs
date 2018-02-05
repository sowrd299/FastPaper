using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Deck))]
public class DeckInspector : Editor 
{

	public override void OnInspectorGUI()
	{
		Deck myTarget = (Deck)target;

		myTarget.deckSize = EditorGUILayout.DelayedIntField("DeckSize", myTarget.deckSize);
		myTarget.tempCard = EditorGUILayout.ObjectField((Object)myTarget.tempCard, typeof(Object), false) as GameObject;

		if(!EditorApplication.isPlaying)
			return;

		Queue<cardScript> cards = myTarget.getCards();
		int x = 0;
		foreach(var item in cards)
		{
			EditorGUILayout.LabelField("Slot#"+x++, item.name);
		}
		
		if(GUILayout.Button("ValidateCards"))
			myTarget.validate();
		if(GUILayout.Button("Shuffle"))
			myTarget.shuffleDeck();
    }
}