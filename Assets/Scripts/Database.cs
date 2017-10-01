using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class Database : MonoBehaviour
{
	static public List<CardInfo> cardDatabase; 

	void Start()
	{
		buildCardDatabase();
	}

	private void buildCardDatabase()
	{
		cardDatabase = new List<CardInfo>();

		string line;
		StreamReader reader = new StreamReader ("Assets/creaturesDatabase.txt");

		line = reader.ReadLine(); //Discard first line

		while (line != null)
		{
			line = reader.ReadLine();

			if (line != null)
			{
				string[] entries = line.Split('\t');
				if (entries.Length > 0)
				{
					CardInfo card = new CardInfo(entries[0], entries[8], int.Parse(entries[3]), int.Parse(entries[1]), int.Parse(entries[4]), int.Parse(entries[5]), int.Parse(entries[6]), int.Parse(entries[7]));
					cardDatabase.Add(card);
				}
			}
		}

		reader.Close();

		//Invoke("LoadGame", 1);
		LoadGame();
	}

	void LoadGame()
	{
		SceneManager.LoadScene("GridTest");
	}
}
