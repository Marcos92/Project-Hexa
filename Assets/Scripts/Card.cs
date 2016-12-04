using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
	//UI
	GameObject canvas;

	GameObject nameLabel;
	GameObject costLabel;
	GameObject descriptionLabel;
	GameObject tribeLabel;

	GameObject attackLabel;
	GameObject healthLabel;
	GameObject speedLabel;
	GameObject rangeLabel;

	public float originalDepth;
	public int originalOrder;
    //TODO: Add original height

	//Data
	string title;
	string description;

	int cost;
	int rarity;

	int initialMaxHealth;
	int initialAttack;
	int initialSpeed;
	int initialRange;

	int maxHealth;
	int attack;
	int speed;
	int range;

	int currentHealth;

	//List with all the card effects

	//Creature card constructor

	//Spell card constructor

	//Trap card constructor

	void Start()
	{
		canvas = transform.FindChild ("Canvas").gameObject;

		originalDepth = transform.localPosition.z;
		originalOrder = GetComponent<SpriteRenderer>().sortingOrder;
		canvas.GetComponent<Canvas>().sortingOrder = originalOrder;

		attackLabel = canvas.transform.FindChild ("AttackLabel").gameObject;
		healthLabel = canvas.transform.FindChild ("HealthLabel").gameObject;
		speedLabel = canvas.transform.FindChild ("SpeedLabel").gameObject;
		rangeLabel = canvas.transform.FindChild ("RangeLabel").gameObject;

		nameLabel = canvas.transform.FindChild ("NameLabel").gameObject;
		costLabel = canvas.transform.FindChild ("CostLabel").gameObject;
		descriptionLabel = canvas.transform.FindChild ("DescriptionLabel").gameObject;
		tribeLabel = canvas.transform.FindChild ("TribeLabel").gameObject;
	}

	public void Create(string _title, string _description, int _cost, int _rarity, int _attack, int _maxHealth, int _speed, int _range)
	{
		title = _title;
		nameLabel.GetComponent<Text>().text= title;

		cost = _cost;
		costLabel.GetComponent<Text>().text= cost.ToString();
	
		description = _description;
		description.Trim ('-');
		descriptionLabel.GetComponent<Text>().text= description;

		//Tribe assignment and label here

		rarity = _rarity;
		//Assign respective rarity gem

		attack = _attack;
		initialAttack = attack;
		attackLabel.GetComponent<Text>().text= attack.ToString();

		maxHealth = _maxHealth;
		initialMaxHealth = maxHealth;
		currentHealth = maxHealth;
		healthLabel.GetComponent<Text>().text= maxHealth.ToString();

		speed = _speed;
		initialSpeed = speed;
		speedLabel.GetComponent<Text>().text= speed.ToString();

		range = _range;
		initialRange = range;
		rangeLabel.GetComponent<Text>().text= range.ToString();

		Print();
	}

	public void Print()
	{
		Debug.Log (title + "\t" + description + "\n" + "Rarity: " + rarity + "\tCost: " + cost + "\tStats: " + attack + "/" + maxHealth + "/" + speed + "/" + range);
	}

	void OnMouseEnter()
	{
		StopCoroutine("HoverDown");
		StartCoroutine("HoverUp");
	}

    void OnMouseExit()
    {
        StopCoroutine("HoverUp");
        StartCoroutine("HoverDown");
    }

    IEnumerator HoverUp()
    {
		BringForward ();

		float finalHeight = 3f;
		float speed = CardBehaviour.cardHoverUpSpeed * Time.deltaTime;
		float zoomSpeed = -CardBehaviour.cardZoomSpeed * Time.deltaTime;

		while(transform.localPosition.y < finalHeight)
		{
			Vector3 p = transform.localPosition;
			transform.localPosition = new Vector3(p.x, p.y + speed, p.z + zoomSpeed);
			speed -= 0.01f;
			yield return null;
		}

		if (transform.localPosition.y > finalHeight) 
		{
			transform.localPosition = new Vector3 (transform.localPosition.x, finalHeight, transform.localPosition.z);
		}
	}

    IEnumerator HoverDown()
    {
		BringBack ();

		float speed = -CardBehaviour.cardHoverDownSpeed * Time.deltaTime;
		float zoomSpeed = CardBehaviour.cardZoomSpeed * Time.deltaTime;

        while (transform.localPosition.y > 0)
        {
            Vector3 p = transform.localPosition;
			transform.localPosition = new Vector3(p.x, p.y + speed, p.z + zoomSpeed);
			yield return null;
        }

        if (transform.localPosition.y < 0) 
		{
			transform.localPosition = new Vector3(transform.localPosition.x, 0, originalDepth);
		}
    }

	void BringForward()
	{
		GetComponent<SpriteRenderer>().sortingOrder = 20;
		canvas.GetComponent<Canvas>().sortingOrder = 20;
	}

	void BringBack()
	{
		GetComponent<SpriteRenderer>().sortingOrder = originalOrder;
		canvas.GetComponent<Canvas>().sortingOrder = originalOrder;
	}
}
