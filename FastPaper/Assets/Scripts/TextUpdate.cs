using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour 
{
	public Scrollbar slider;

	private Text text;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	public void UpdateText()
	{
		if(slider.value*3 == 3)
			text.text = ((CardType)2).ToString();
		else
			text.text = ((CardType)(slider.value*3)).ToString();
	}

}
