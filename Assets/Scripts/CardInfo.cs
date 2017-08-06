using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo 
{
	public string title;
	public string description;

	public int cost;
	public int rarity;

	public int health;
	public int attack;
	public int speed;
	public int range;

	public CardInfo(string _title, string _description, int _cost, int _rarity, int _attack, int _health, int _speed, int _range)
	{
		title = _title;

		cost = _cost;
	
		description = _description;
		description.Trim ('-');

		//Tribe assignment

		rarity = _rarity;

		attack = _attack;
		health = _health;
		speed = _speed;
		range = _range;
	}
}
