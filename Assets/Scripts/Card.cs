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
	public CardInfo info;

	string title;
	string description;

	int cost;
	int rarity;

	int initialMaxHealth;
	int initialAttack;
	int initialSpeed;
	int initialRange;

	int currentHealth;
	int maxHealth;
	int attack;
	int speed;
	int range;

	//List with all the card effects

	//Events
    public delegate void ClickCell(Card c); 
    public static event ClickCell OnClickCard;

	public Card(CardInfo i)
	{
		Create(i);
	}

	void Start()
	{
		
	}

	public CardInfo GetCurrentInfo() //When cards are modified while in deck we need to get the current stats, not the original ones
	{
		CardInfo i = info;

		i.attack = attack;
		i.currentHealth = currentHealth;
		i.maxHealth = maxHealth;
		i.speed = speed;
		i.range = range;

		return i;
	}

	//Creature card constructor
	public void Create(CardInfo i)
	{
		info = i;

		title = info.title;

		cost = info.cost;
	
		description = info.description;

		//Tribe assignment

		rarity = info.rarity;

		attack = info.attack;
		initialAttack = attack;

		maxHealth = info.maxHealth;
		initialMaxHealth = maxHealth;
		currentHealth = info.currentHealth;

		speed = info.speed;
		initialSpeed = speed;

		range = info.range;
		initialRange = range;
	}

	//Spell card constructor

	//Trap card constructor

	public void CreateVisual()
	{
		canvas = transform.Find ("Canvas").gameObject;

		originalDepth = transform.localPosition.z;
		originalHeight = transform.localPosition.y;
		originalOrder = GetComponent<SpriteRenderer>().sortingOrder;
		canvas.GetComponent<Canvas>().sortingOrder = originalOrder;

		attackLabel = canvas.transform.Find ("AttackLabel").gameObject;
		healthLabel = canvas.transform.Find ("HealthLabel").gameObject;
		speedLabel = canvas.transform.Find ("SpeedLabel").gameObject;
		rangeLabel = canvas.transform.Find ("RangeLabel").gameObject;

		nameLabel = canvas.transform.Find ("NameLabel").gameObject;
		costLabel = canvas.transform.Find ("CostLabel").gameObject;
		descriptionLabel = canvas.transform.Find ("DescriptionLabel").gameObject;
		tribeLabel = canvas.transform.Find ("TribeLabel").gameObject;
		
		nameLabel.GetComponent<Text>().text = title;
		costLabel.GetComponent<Text>().text = cost.ToString();
		descriptionLabel.GetComponent<Text>().text = description;
		attackLabel.GetComponent<Text>().text = attack.ToString();
		healthLabel.GetComponent<Text>().text = maxHealth.ToString();
		speedLabel.GetComponent<Text>().text = speed.ToString();
		rangeLabel.GetComponent<Text>().text = range.ToString();
	}

	public void Print()
	{
		Debug.Log (title + "\t" + description + "\n" + "Rarity: " + rarity + "\tCost: " + cost + "\tStats: " + attack + "/" + maxHealth + "/" + speed + "/" + range);
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

	//Mouse events

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

    void OnMouseDown()
    {
        OnClickCard(this);
    }
}
