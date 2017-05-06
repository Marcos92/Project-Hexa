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
	public float originalHeight;
	public int originalOrder;

	float cardHoverUpSpeed = 15f;
	float cardHoverDownSpeed = 20f;
	float cardZoomSpeed = 1f;

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
	public Card(string _title, string _description, int _cost, int _rarity, int _attack, int _maxHealth, int _speed, int _range)
	{
		title = _title;

		cost = _cost;
	
		description = _description;
		description.Trim ('-');

		//Tribe assignment

		rarity = _rarity;

		attack = _attack;
		initialAttack = attack;

		maxHealth = _maxHealth;
		initialMaxHealth = maxHealth;
		currentHealth = maxHealth;

		speed = _speed;
		initialSpeed = speed;

		range = _range;
		initialRange = range;
	}

	//Spell card constructor

	//Trap card constructor

	void Start()
	{
		canvas = transform.FindChild ("Canvas").gameObject;

		originalDepth = transform.localPosition.z;
		originalHeight = transform.localPosition.y;
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

	/*public void Create(string _title, string _description, int _cost, int _rarity, int _attack, int _maxHealth, int _speed, int _range)
	{
		title = _title;

		cost = _cost;
	
		description = _description;
		description.Trim ('-');

		//Tribe assignment

		rarity = _rarity;

		attack = _attack;
		initialAttack = attack;

		maxHealth = _maxHealth;
		initialMaxHealth = maxHealth;
		currentHealth = maxHealth;

		speed = _speed;
		initialSpeed = speed;

		range = _range;
		initialRange = range;
	}*/

	public void Create()
	{
		nameLabel.GetComponent<Text>().text= title;
		costLabel.GetComponent<Text>().text= cost.ToString();
		descriptionLabel.GetComponent<Text>().text= description;
		attackLabel.GetComponent<Text>().text= attack.ToString();
		healthLabel.GetComponent<Text>().text= maxHealth.ToString();
		speedLabel.GetComponent<Text>().text= speed.ToString();
		rangeLabel.GetComponent<Text>().text= range.ToString();
	}

	public void Print()
	{
		Debug.print (title + "\t" + description + "\n" + "Rarity: " + rarity + "\tCost: " + cost + "\tStats: " + attack + "/" + maxHealth + "/" + speed + "/" + range);
	}

	void OnMouseEnter()
	{
		/*StopCoroutine("HoverDown");
		StartCoroutine("HoverUp");*/
	}

    void OnMouseExit()
    {
        /*StopCoroutine("HoverUp");
        StartCoroutine("HoverDown");*/
    }

    IEnumerator HoverUp()
    {
		BringForward ();

		float finalHeight = originalHeight + 2f;
		float speed = cardHoverUpSpeed * Time.deltaTime;
		float zoomSpeed = -cardZoomSpeed * Time.deltaTime;

		while(transform.localPosition.y < finalHeight)
		{
			Vector3 p = transform.localPosition;
			transform.localPosition += transform.up.normalized * speed + transform.forward.normalized * zoomSpeed;
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

		float speed = -cardHoverDownSpeed * Time.deltaTime;
		float zoomSpeed = cardZoomSpeed * Time.deltaTime;

        while (transform.localPosition.y > originalHeight)
        {
            Vector3 p = transform.localPosition;
			transform.localPosition += transform.up.normalized * speed + transform.forward.normalized * zoomSpeed;
			yield return null;
        }

        if (transform.localPosition.y < originalHeight) 
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
