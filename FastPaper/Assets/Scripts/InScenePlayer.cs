using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InScenePlayer : MonoBehaviour 
{
	public TextMesh nameText;
	public TextMesh healthText;
	public TextMesh pipText;

	void Awake()
	{
		nameText = transform.Find("PlayerName").gameObject.GetComponent<TextMesh>();
		healthText = transform.Find("PlayerHealth").gameObject.GetComponent<TextMesh>();
		pipText = transform.Find("PlayerPips").gameObject.GetComponent<TextMesh>();
	}

}
