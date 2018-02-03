using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		GameManager myTarget = (GameManager)target;
		//DrawDefaultInspector();
		myTarget.Player1MaxHealth = EditorGUILayout.DelayedIntField("Player1 Max HP", myTarget.Player1MaxHealth);
		myTarget.Player2MaxHealth = EditorGUILayout.DelayedIntField("Player2 Max HP", myTarget.Player2MaxHealth);

		EditorGUILayout.Slider(myTarget.Player1Health, 0, myTarget.Player1MaxHealth);
		EditorGUILayout.Slider(myTarget.Player2Health, 0, myTarget.Player2MaxHealth);

		if(!EditorApplication.isPlaying)
			return;
		if(GUILayout.Button("AdvanceTurn"))
		{
			myTarget.advanceTurn();
		}
    }
}