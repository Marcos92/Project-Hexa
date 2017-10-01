using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	//public static GameManager instance = null;

	private static CardManager cards;
	private static BoardManager board;
	private static UIManager ui;

	void Awake()
	{
		/* if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject); */
		
		//DontDestroyOnLoad(gameObject);
		
		cards = GetComponent<CardManager>();
		board = GetComponent<BoardManager>();
		ui = GetComponent<UIManager>();
	}

	public static Card GetSelectedCard()
	{
		if(cards.selectedCard != null)
		{
			return cards.selectedCard.GetComponent<Card>();
		}

		return null;
	}

	public static bool CanPlayCard()
	{
		return true;

		//Change later to check card type and condition (mana cost, targets, etc)
	}

	public static void PlayCard(Cell cell)
	{
		board.Summon(cell, GetSelectedCard(), true);
		cards.Play();
	}
}
