using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor 
{
	protected bool playerOneFoldout = false;
	protected bool playerTwoFoldout = false;
	protected bool triggerFoldout = false;

	public override void OnInspectorGUI()
	{
		GameManager manager = (GameManager)target;
		//DrawDefaultInspector();

		PlayerInfo p1 = manager.playerOne;
		PlayerInfo p2 = manager.playerTwo;

		if(!EditorApplication.isPlaying)
		{
			manager.playerOnePrefab = EditorGUILayout.ObjectField((UnityEngine.Object)manager.playerOnePrefab, typeof(UnityEngine.Object), false) as ScriptableObject;
			manager.playerTwoPrefab = EditorGUILayout.ObjectField((UnityEngine.Object)manager.playerTwoPrefab, typeof(UnityEngine.Object), false) as ScriptableObject;
			return;
		}

		playerOneFoldout = EditorGUILayout.Foldout(playerOneFoldout, "Player 1", false);
		if(playerOneFoldout)
		{
			EditorGUI.indentLevel++;
			p1.maxHealth = EditorGUILayout.DelayedIntField("Max Health", p1.maxHealth);
			p1.currentHealth = EditorGUILayout.DelayedIntField("Current Health", p1.currentHealth);
			p1.pips = EditorGUILayout.DelayedIntField("Pips", p1.pips);
			EditorGUI.indentLevel--;
		}

		playerTwoFoldout = EditorGUILayout.Foldout(playerTwoFoldout, "Player 2", false);
		if(playerTwoFoldout)
		{
			EditorGUI.indentLevel++;
			p2.maxHealth = EditorGUILayout.DelayedIntField("Max Health", p2.maxHealth);
			p2.currentHealth = EditorGUILayout.DelayedIntField("Current Health", p2.currentHealth);
			p2.pips = EditorGUILayout.DelayedIntField("Pips", p2.pips);
			EditorGUI.indentLevel--;
		}

		triggerFoldout = EditorGUILayout.Foldout(triggerFoldout, "Triggers", false);
		if(triggerFoldout)
		{
			EditorGUI.indentLevel++;
			Delegate[] temp = manager.onSpiritPlay.GetInvocationList();
			EditorGUILayout.LabelField("OnSpiritPlay");
			EditorGUI.indentLevel++;
			for(int x = 0; x < temp.Length; x++)
				EditorGUILayout.LabelField(temp[x].Target.ToString());
			EditorGUI.indentLevel--;

			temp = manager.onAttack.GetInvocationList();
			EditorGUILayout.LabelField("OnAttack");
			EditorGUI.indentLevel++;
			for(int x = 0; x < temp.Length; x++)
				EditorGUILayout.LabelField(temp[x].Target.ToString());
			EditorGUI.indentLevel--;

			temp = manager.onSpiritFade.GetInvocationList();
			EditorGUILayout.LabelField("OnSpritFade");
			EditorGUI.indentLevel++;
			for(int x = 0; x < temp.Length; x++)
				EditorGUILayout.LabelField(temp[x].Target.ToString());
			EditorGUI.indentLevel--;

			EditorGUI.indentLevel--;
		}


		SerializedObject obj = new UnityEditor.SerializedObject(manager);
		EditorGUILayout.LabelField("Current Phase:", Enum.GetName(typeof(Turn), obj.FindProperty("currTurn").enumValueIndex));
		EditorGUILayout.LabelField("CanPlay:", manager.canPlay.ToString());
		EditorGUILayout.LabelField("Current Attack:", manager.currAttack.ToString());

		if(GUILayout.Button("AdvanceTurn"))
		{
			manager.advanceTurn();
		}
    }
}