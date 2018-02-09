using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardEditor : EditorWindow
{

	private CardScriptable toCreate;

	private int triggerIndex = 0;
	private int effectIndex = 0;

	[MenuItem("Window/Cards")]
	public static void Init()
	{
		GetWindow(typeof(CardEditor));
	}

	void OnGUI()
	{
		if(EditorApplication.isPlayingOrWillChangePlaymode)
			return;
		if(toCreate == null)
		{
			toCreate = ScriptableObject.CreateInstance<CardScriptable>();
			return;
		}
		else
		{
			toCreate.name = EditorGUILayout.DelayedTextField("Name:", toCreate.name);
			toCreate.flavorText = EditorGUILayout.DelayedTextField("Flavor Text:", toCreate.flavorText);
			toCreate.textBox = EditorGUILayout.DelayedTextField("Textbox:", toCreate.textBox);
			toCreate.attack = EditorGUILayout.DelayedIntField("Attack:", toCreate.attack);
			toCreate.countdown = EditorGUILayout.DelayedIntField("Countdown:", toCreate.countdown);
			toCreate.cost = EditorGUILayout.DelayedIntField("Cost:", toCreate.cost);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Sprite:");
			toCreate.picture = EditorGUILayout.ObjectField(toCreate.picture, typeof(Sprite), false) as Sprite;
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("Effects");
			EditorGUI.indentLevel++;
			foreach(TriggeredAbility item in toCreate.abilities)
				EditorGUILayout.LabelField(item.ToString());
			EditorGUI.indentLevel--;

			EditorGUILayout.BeginHorizontal();
			triggerIndex = EditorGUILayout.Popup(triggerIndex, Enum.GetNames(typeof(Triggers)));
			effectIndex = EditorGUILayout.Popup(effectIndex, Enum.GetNames(typeof(PossibleEffects)));
			if(GUILayout.Button("Add Effect"))
				toCreate.abilities.Add(new TriggeredAbility((Triggers)triggerIndex, (PossibleEffects)effectIndex));
			EditorGUILayout.EndHorizontal();

			if(GUILayout.Button("Make the Card!") && toCreate != null)
			{
				AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Cards/" + toCreate.name + ".asset");
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}

			if(GUILayout.Button("Reset!"))
				toCreate = null;
		}
		
	}
}