using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldHandler))]
public class FieldInspector : Editor 
{
	protected bool hitfield = false;
	protected bool blockField = false;
	protected bool grabField = false;

	public override void OnInspectorGUI()
	{
		FieldHandler myTarget = (FieldHandler)target;

		hitfield = EditorGUILayout.Foldout(hitfield, "HitArea", false);
		if(hitfield)
		{
			List<InSceneCard> temp = myTarget.hitCards;
			EditorGUI.indentLevel++;
			foreach(var item in temp)
				EditorGUILayout.LabelField("Card:", item.cardInfo.name);
			EditorGUI.indentLevel--;
		}
		blockField = EditorGUILayout.Foldout(blockField, "BlockArea", false);
		if(blockField)
		{
			List<InSceneCard> temp = myTarget.blockCards;
			EditorGUI.indentLevel++;
			foreach(var item in temp)
				EditorGUILayout.LabelField("Card:", item.cardInfo.name);
			EditorGUI.indentLevel--;
		}
		grabField = EditorGUILayout.Foldout(grabField, "GrabArea", false);
		if(grabField)
		{
			List<InSceneCard> temp = myTarget.grabCards;
			EditorGUI.indentLevel++;
			foreach(var item in temp)
				EditorGUILayout.LabelField("Card:", item.cardInfo.name);
			EditorGUI.indentLevel--;;
		}
	}
}
