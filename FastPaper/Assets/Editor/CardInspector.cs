using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InSceneCard))]
public class CardInspector : Editor 
{

	public override void OnInspectorGUI()
	{
		InSceneCard myTarget = (InSceneCard)target;

		myTarget.cardInfo = EditorGUILayout.ObjectField((UnityEngine.Object)myTarget.cardInfo, typeof(UnityEngine.Object), false) as CardScriptable;

		if(myTarget.cardInfo == null)
			return;
		
		CardScriptable card = myTarget.cardInfo;

		EditorGUILayout.DelayedTextField("Name:", card.name);
		EditorGUILayout.DelayedTextField("Flavor Text:", card.flavorText);
		EditorGUILayout.DelayedTextField("Textbox:", card.textBox);
		EditorGUILayout.DelayedIntField("Attack:", card.attack);
		EditorGUILayout.DelayedIntField("Countdown:", card.countdown);
		EditorGUILayout.DelayedIntField("Cost:", card.cost);
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Sprite:");
		EditorGUILayout.ObjectField(card.picture, typeof(Sprite), false);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Effects");
		EditorGUI.indentLevel++;
		foreach(TriggeredAbility item in card.abilities)
			EditorGUILayout.LabelField(item.ToString());
		EditorGUI.indentLevel--;

    }
}
