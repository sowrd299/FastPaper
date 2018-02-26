using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(NetworkedPlayer))]
public class NetworkPlayerInspector : Editor
{
	public override void OnInspectorGUI()
	{
		NetworkedPlayer myTarget = (NetworkedPlayer)target;
		EditorGUILayout.LabelField("NUMPLAYERS:", NetworkedPlayer.numInstPlayers.ToString());
	}
}
