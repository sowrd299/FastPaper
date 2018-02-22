using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardScriptable))]
public class CardAssetViewer : Editor 
{

	private int triggerIndex = 0;
	private int effectIndex = 0;
	private int typeIndex = 0;

	public override void OnInspectorGUI()
	{
		CardScriptable myTarget = (CardScriptable)target;
		EditorUtility.SetDirty(myTarget);

		myTarget.name = EditorGUILayout.DelayedTextField("Name:", myTarget.name);
		myTarget.flavorText = EditorGUILayout.DelayedTextField("Flavor Text:", myTarget.flavorText);
		myTarget.textBox = EditorGUILayout.DelayedTextField("Textbox:", myTarget.textBox);
		myTarget.attack = EditorGUILayout.DelayedIntField("Attack:", myTarget.attack);
		myTarget.countdown = EditorGUILayout.DelayedIntField("Countdown:", myTarget.countdown);
		myTarget.cost = EditorGUILayout.DelayedIntField("Cost:", myTarget.cost);
		myTarget.type = (CardType)EditorGUILayout.Popup("Type: ", (int)myTarget.type, Enum.GetNames(typeof(CardType)));
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Sprite:");
		myTarget.picture = EditorGUILayout.ObjectField(myTarget.picture, typeof(Sprite), false) as Sprite;
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Effects");
		EditorGUI.indentLevel++;
		foreach(TriggeredAbility item in myTarget.abilities)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(item.ToString());
			if(GUILayout.Button("Remove"))
				myTarget.abilities.Remove(item);
			EditorGUILayout.EndHorizontal();
		}
		EditorGUI.indentLevel--;

		EditorGUILayout.BeginHorizontal();
		triggerIndex = EditorGUILayout.Popup(triggerIndex, Enum.GetNames(typeof(Triggers)));
		effectIndex = EditorGUILayout.Popup(effectIndex, Enum.GetNames(typeof(PossibleEffects)));
		if(GUILayout.Button("Add Effect"))
			myTarget.abilities.Add(new TriggeredAbility((Triggers)triggerIndex, (PossibleEffects)effectIndex));
		EditorGUILayout.EndHorizontal();

		if(GUILayout.Button("Save"))
		{
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

    }
}
