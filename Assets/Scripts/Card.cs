using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
	public enum Type { Creature, Spell, Trap }
    public Type type;

	string title;
	string description;

	int cost;
	int rarity;

	//Creature cards only
	int initialMaxHealth;
	int initialAttack;
	int initialSpeed;
	int initialRange;

	int maxHealth;
	int attack;
	int speed;
	int range;

	int currentHealth;

	Creature creature;

	//List with all the card effects

	//Creature card constructor

	//Spell card constructor

	//Trap card constructor

	public void create(string _title, string _description, int _cost, int _rarity, int _maxHealth, int _attack, int _speed, int _range)
	{
		title = _title;
		transform.FindChild ("NameLabel").GetComponent <TextMesh>().text = _title;

		description = _description;
		transform.FindChild ("DescriptionLabel").GetComponent <TextMesh>().text = _description;

		cost = _cost;
		transform.FindChild ("ManaLabel").GetComponent <TextMesh>().text = _cost.ToString();

		rarity = _rarity;
		//Do stuff

		maxHealth = _maxHealth;
		currentHealth = maxHealth;
		transform.FindChild ("HealthLabel").GetComponent <TextMesh>().text = _maxHealth.ToString();

		attack = _attack;
		transform.FindChild ("AttackLabel").GetComponent <TextMesh>().text = _attack.ToString();

		speed = _speed;
		transform.FindChild ("SpeedLabel").GetComponent <TextMesh>().text = _speed.ToString();

		range = _range;
		transform.FindChild ("RangeLabel").GetComponent <TextMesh>().text = _range.ToString();

		type = Type.Creature;

		print();
	}

	public void print()
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
}
