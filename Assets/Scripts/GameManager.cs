using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	CardManager cards;
	BoardManager board;
	UIManager ui;

	// Use this for initialization
	void Start () 
	{
		cards = new CardManager();
		board = new BoardManager();
		ui = new UIManager();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
