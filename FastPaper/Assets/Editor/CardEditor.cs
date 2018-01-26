using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(cardScript))]
public class CardEditor : Editor {

	public override void OnInspectorGUI()
	{
		cardScript card = (cardScript)target;
		DrawDefaultInspector();
		EditorGUILayout.LabelField("Level", card.Level.ToString());
    }
}