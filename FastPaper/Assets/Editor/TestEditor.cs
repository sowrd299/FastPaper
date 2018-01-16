using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(cardScript))]
public class TestEditor : Editor {

	public override void OnInspectorGUI()
	{
		cardScript myTarget = (cardScript)target;
		DrawDefaultInspector();
		//myTarget.experience = EditorGUILayout.IntField("Experince",myTarget.experience);
		EditorGUILayout.LabelField("Level",myTarget.Level.ToString());
        EditorGUILayout.HelpBox("Testing custome inspector tech, don't be to mindful of me", MessageType.Info);
		if(GUILayout.Button("huh"))
		{
			myTarget.BuildObject();
		}
    }
}
