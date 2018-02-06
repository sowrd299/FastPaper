using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "card", menuName = "NewCard")]
public class CardScriptable : ScriptableObject {
	public string name;
	public string flavorTest;
	public int lifeTime;
	public Sprite picture;

}
