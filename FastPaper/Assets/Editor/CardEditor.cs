using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardEditor : EditorWindow
{

	private CardScriptable toCreate;
	private string[] possibleTriggers = { "a", "b", "c" };
	private int triggerIndex = 0;
	private int effectIndex = 0;

	[MenuItem("Window/Cards")]
	public static void Init()
	{
		GetWindow(typeof(CardEditor));
	}

	void OnGUI()
	{
		if(toCreate == null)
			toCreate = ScriptableObject.CreateInstance<CardScriptable>();
		else
		{
			toCreate.name = EditorGUILayout.DelayedTextField("Name:", toCreate.name);
			toCreate.flavorText = EditorGUILayout.DelayedTextField("Flavor Text:", toCreate.flavorText);
			toCreate.textBox = EditorGUILayout.DelayedTextField("Textbox:", toCreate.textBox);
			toCreate.attack = EditorGUILayout.DelayedIntField("Attack:", toCreate.attack);
			toCreate.countdown = EditorGUILayout.DelayedIntField("Countdown:", toCreate.countdown);
			toCreate.cost = EditorGUILayout.DelayedIntField("Cost:", toCreate.cost);
			toCreate.picture = EditorGUILayout.ObjectField(toCreate.picture, typeof(Sprite), false) as Sprite;

			EditorGUILayout.BeginHorizontal();
			triggerIndex = EditorGUILayout.Popup(triggerIndex, possibleTriggers);
			effectIndex = EditorGUILayout.Popup(effectIndex, possibleTriggers);
			if(GUILayout.Button("add effect"))
				return;
			EditorGUILayout.EndHorizontal();
		}
		if(GUILayout.Button("Make the Card!") && toCreate != null)
		{
			AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Cards/" + toCreate.name + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}