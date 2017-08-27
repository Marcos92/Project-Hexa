using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;

	private static CardManager cards;
	private static BoardManager board;
	private static UIManager ui;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
		
		//DontDestroyOnLoad(gameObject);
		
		cards = GetComponent<CardManager>();
		board = GetComponent<BoardManager>();
		ui = GetComponent<UIManager>();
	}

	public static Card GetSelectedCard()
	{
		return cards.selectedCard;
	}
}
