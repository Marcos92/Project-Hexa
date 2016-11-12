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

	float originalDepth;
	int originalLayerOrder;

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

	public void Create(string _title, string _description, int _cost, int _rarity, int _attack, int _maxHealth, int _speed, int _range)
	{
		canvas = transform.FindChild ("Canvas").gameObject;

		originalDepth = transform.localPosition.z;
		originalLayerOrder = transform.GetComponent<SpriteRenderer> ().sortingOrder;

		attackLabel = canvas.transform.FindChild ("AttackLabel").gameObject;
		healthLabel = canvas.transform.FindChild ("HealthLabel").gameObject;
		speedLabel = canvas.transform.FindChild ("SpeedLabel").gameObject;
		rangeLabel = canvas.transform.FindChild ("RangeLabel").gameObject;

		nameLabel = canvas.transform.FindChild ("NameLabel").gameObject;
		costLabel = canvas.transform.FindChild ("CostLabel").gameObject;
		descriptionLabel = canvas.transform.FindChild ("DescriptionLabel").gameObject;
		tribeLabel = canvas.transform.FindChild ("TribeLabel").gameObject;

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
		Vector3 v = transform.localPosition;
		transform.localPosition = new Vector3 (v.x, v.y, -1f);

		transform.GetComponent<SpriteRenderer> ().sortingOrder = 20;

        while(transform.localPosition.y < 2)
        {
            Vector3 p = transform.localPosition;
			float speed = CardBehaviour.cardHoverUpSpeed / (p.y + 1f) * Time.deltaTime;
            transform.localPosition = new Vector3(p.x, p.y + speed, p.z);
            yield return null;
        }

		if (transform.localPosition.y > 2) 
		{
			transform.localPosition = new Vector3 (transform.localPosition.x, 2, transform.localPosition.z);
		}
    }

    IEnumerator HoverDown()
    {
		Vector3 v = transform.localPosition;
		transform.localPosition = new Vector3 (v.x, v.y, originalDepth);

		transform.GetComponent<SpriteRenderer> ().sortingOrder = originalLayerOrder;

		float speed = -CardBehaviour.cardHoverDownSpeed * Time.deltaTime;

        while (transform.localPosition.y > 0)
        {
            Vector3 p = transform.localPosition;
            transform.localPosition = new Vector3(p.x, p.y + speed, p.z);
            yield return null;
        }

        if (transform.localPosition.y < 0) 
		{
			transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
		}
    }

	void BringForward()
	{
		
	}

	void BringBack()
	{
		
	}
}
