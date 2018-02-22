using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeckEditor : EditorWindow
{

	private DeckList toCreate;
	private CardScriptable toAdd = null;

	[MenuItem("Window/Decks")]
	public static void Init()
	{
		GetWindow(typeof(DeckEditor));
	}

	void OnGUI()
	{
		if(EditorApplication.isPlayingOrWillChangePlaymode)
			return;
		if(toCreate == null)
		{
			toCreate = ScriptableObject.CreateInstance<DeckList>();
			return;
		}
		else
		{
			toCreate.deckName = EditorGUILayout.DelayedTextField("Name:", toCreate.deckName);

			EditorGUILayout.LabelField("Current Cards");
			EditorGUI.indentLevel++;
			foreach(CardScriptable item in toCreate.deckList)
				EditorGUILayout.LabelField(item.ToString());
			EditorGUI.indentLevel--;

			
			EditorGUILayout.BeginHorizontal();
			toAdd = EditorGUILayout.ObjectField(toAdd, typeof(CardScriptable), false) as CardScriptable;
			if(GUILayout.Button("Add Card") && toAdd != null)
			{
				toCreate.deckList.Add(toAdd);
				//toAdd = null;
			}
			EditorGUILayout.EndHorizontal();

			if(GUILayout.Button("Make the Deck!") && toCreate != null)
			{
				AssetDatabase.CreateAsset(toCreate, "Assets/ScriptableAssets/Decks/" + toCreate.deckName + ".asset");
				toCreate = null;
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
				return;
			}

			if(GUILayout.Button("Reset!"))
				toCreate = null;
		}
		
	}
}