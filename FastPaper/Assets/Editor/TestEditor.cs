using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandHandler))]
public class TestEditor : Editor 
{

	public override void OnInspectorGUI()
	{
		HandHandler myTarget = (HandHandler)target;
		DrawDefaultInspector();

		if(!EditorApplication.isPlaying)
			return;
		List<cardScript> cards = myTarget.getCards();
		for(int x = 0; x < cards.Count; x++)
			EditorGUILayout.LabelField("Card"+x, cards[x] != null ? cards[x].name : "empty");
		if(GUILayout.Button("Update Hands"))
		{
			myTarget.UpdateSlots();
		}
    }
}
