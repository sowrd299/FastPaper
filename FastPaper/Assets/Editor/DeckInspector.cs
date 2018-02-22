using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckHandler))]
public class DeckInspector : Editor 
{

	public override void OnInspectorGUI()
	{
		DeckHandler myTarget = (DeckHandler)target;
		EditorUtility.SetDirty(myTarget);

		myTarget.emptyCardPrefab = EditorGUILayout.ObjectField("EmptyCard:", myTarget.emptyCardPrefab, typeof(GameObject), false) as GameObject;
		myTarget.deckList = EditorGUILayout.ObjectField("Deck List:", myTarget.deckList, typeof(DeckList), false) as DeckList;

		if(!EditorApplication.isPlaying)
			return;

		Queue<InSceneCard> cards = myTarget.getCards();
		int x = 0;
		foreach(var item in cards)
		{
			EditorGUILayout.LabelField("Slot#"+x++, item.cardInfo.name);
		}
		
		if(GUILayout.Button("ValidateCards"))
			myTarget.validate();
		if(GUILayout.Button("Shuffle"))
			myTarget.shuffleDeck();
    }
}