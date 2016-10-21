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
	public Card (string title, string description, int cost, int rarity, int maxHealth, int attack, int speed, int range)
	{
		this.title = title;
		this.description = description;
		this.cost = cost;
		this.rarity = rarity;
		this.maxHealth = maxHealth;
		this.attack = attack;
		this.speed = speed;
		this.range = range;

		type = Type.Creature;
	}

	//Spell card constructor

	//Trap card constructor

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

        if (transform.localPosition.y > 2) transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
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

        if (transform.localPosition.y < 0) transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }
}
