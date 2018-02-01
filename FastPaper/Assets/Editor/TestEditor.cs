using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Deck))]
public class TestEditor : Editor 
{
	public cardScript lastCardDrawn;

	public override void OnInspectorGUI()
	{
		Deck myTarget = (Deck)target;
		DrawDefaultInspector();
/*		Queue<cardScript> temp = myTarget.cards;
		for(int x = 0; temp.Count > 0; x++)
			EditorGUILayout.LabelField("Card"+x, temp.Dequeue().name);
*/		if(GUILayout.Button("TestDraw"))
		{
			lastCardDrawn = myTarget.drawCard();
			EditorGUILayout.LabelField("lastcarddrawn", lastCardDrawn.name);
		}
		if(GUILayout.Button("AddCard"))
		{
			myTarget.addCard();
		}
		EditorGUILayout.LabelField("lastcarddrawn", lastCardDrawn == null ? "none" : lastCardDrawn.name);
    }
}
