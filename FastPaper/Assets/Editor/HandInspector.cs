using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandHandler))]
public class HandInspector : Editor 
{

	public override void OnInspectorGUI()
	{
		HandHandler myTarget = (HandHandler)target;
		DrawDefaultInspector();

		if(!EditorApplication.isPlaying)
			return;

		List<InSceneCard> cards = myTarget.getCards();
		for(int x = 0; x < myTarget.handSize; x++)
			if(x >= cards.Count)
				EditorGUILayout.LabelField("Slot"+x, "empty");
			else
				EditorGUILayout.LabelField("Slot"+x, cards[x].cardInfo.name);

		if(GUILayout.Button("Update Hands"))
		{
			myTarget.UpdateSlots();
		}
    }
}
