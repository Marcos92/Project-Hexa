using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour 
{
	private List<Card> cards; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Build()
	{
		for(int i = 0; i < 30; i++)
		{
			cards.Add(Database.cardDatabase[i]);
		}

		Print();
	}

	public int Count()
	{
		return cards.Count;
	}

	public void Draw(int amount)
	{

	}

	public void Shuffle()
	{

	}

	public void ShuffleInto(Card[] cards)
	{

	}

	void Print()
	{
		foreach(Card c in cards)
		{
			c.Print();
		}
	}
}
