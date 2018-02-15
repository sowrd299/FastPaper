using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InSceneCard : MonoBehaviour 
{
	public CardScriptable cardInfo;
	public bool isCastable;

	private TextMesh nameText;
	private TextMesh cardText;
	private TextMesh typeText;
	private TextMesh attackText;
	private TextMesh countdownText;
	private TextMesh costText;
	private SpriteRenderer sprite;

	void Start () 
	{
		isCastable = true;
		cardInfo = Object.Instantiate(cardInfo);

		nameText = transform.Find("NameText").gameObject.GetComponent<TextMesh>();
		cardText = transform.Find("CardText").gameObject.GetComponent<TextMesh>();
		typeText = transform.Find("TypeText").gameObject.GetComponent<TextMesh>();
		attackText = transform.Find("AttackText").gameObject.GetComponent<TextMesh>();
		countdownText = transform.Find("CountdownText").gameObject.GetComponent<TextMesh>();
		costText = transform.Find("CostText").gameObject.GetComponent<TextMesh>();
		sprite = transform.Find("CardImage").gameObject.GetComponent<SpriteRenderer>();

		UpdateCardInfo();
	}

	public void UpdateCardInfo()
	{
		nameText.text = cardInfo.name;
		cardText.text = cardInfo.textBox;
		typeText.text = cardInfo.type.ToString();
		attackText.text = cardInfo.attack.ToString();
		countdownText.text = cardInfo.countdown.ToString();
		costText.text = cardInfo.cost.ToString();
		sprite.sprite = cardInfo.picture;
	}
	
	void OnDestroy()
	{
		OnDeath();
	}

	public bool canCast()
	{
		if(GameManager.instance.getCurrentTurn() == Turn.p1Play && GameManager.instance.playerOne.pips >= cardInfo.cost)
		{
			return true;
		}
		else if(GameManager.instance.getCurrentTurn() == Turn.p2Play && GameManager.instance.playerTwo.pips >= cardInfo.cost)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void Cast()
	{	
		if(GameManager.instance.getCurrentTurn() == Turn.p1Play) { GameManager.instance.playerOne.pips -= cardInfo.cost; }
		if(GameManager.instance.getCurrentTurn() == Turn.p2Play) { GameManager.instance.playerTwo.pips -= cardInfo.cost; }
		isCastable = false;

		foreach (var item in cardInfo.abilities)
		{
			switch(item.trigger)
			{
				case Triggers.Opener:
				{
					Debug.Log("opener trigger created");
					item.TriggerAbility(this);
				} break;
				case Triggers.Fade:
				{
					Debug.Log("fade trigger created");
					GameManager.instance.onSpiritFade += item.TriggerAbility;
				} break;
				case Triggers.OnPersonalAttack:
				{
					Debug.Log("onattack trigger created");
					GameManager.instance.onAttack += item.TriggerAbility;
				} break;
				default:
				{

				} break;
			}
		}
	}

	public void OnDeath()
	{
		GameManager.instance.onSpiritFade(this);
		Destroy(gameObject);
	}
}
