using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InSceneCard))]
public class CardInspector : Editor 
{
	public override void OnInspectorGUI()
	{
		//DrawDefaultInspector();
		
		InSceneCard myTarget = (InSceneCard)target;

		myTarget.cardInfo = (EditorGUILayout.ObjectField((UnityEngine.Object)myTarget.cardInfo, typeof(UnityEngine.Object), false) as CardScriptable);
		Debug.Log(myTarget.cardInfo);
		if(myTarget.cardInfo == null)
			return;
		
		CardScriptable card = myTarget.cardInfo;

		EditorGUILayout.LabelField("Name:", card.name);
		EditorGUILayout.LabelField("Flavor Text:", card.flavorText);
		EditorGUILayout.LabelField("Textbox:", card.textBox);
		EditorGUILayout.LabelField("Attack:", card.attack.ToString());
		EditorGUILayout.LabelField("Countdown:", card.countdown.ToString());
		EditorGUILayout.LabelField("Cost:", card.cost.ToString());
		
		EditorGUILayout.LabelField("Type:", card.type.ToString());


		EditorGUILayout.LabelField("Effects");
		EditorGUI.indentLevel++;
		foreach(TriggeredAbility item in card.abilities)
			EditorGUILayout.LabelField(item.ToString());
		EditorGUI.indentLevel--;

		if(GUILayout.Button("Kill this card"))
		{
			Destroy(myTarget.gameObject);
		}

    }
}
