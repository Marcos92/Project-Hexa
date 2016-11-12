using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class Database : MonoBehaviour
{
	public GameObject cardTemplate;

	static public List<GameObject> cardDatabase; 

	public void buildCardDatabase()
	{
		cardDatabase = new List<GameObject> ();

		string line;
		StreamReader reader = new StreamReader ("Assets/creaturesDatabase.txt");

		line = reader.ReadLine(); //Discard first line

		Vector3 cardPosition = new Vector3 (0, 0, 0);

		while (line != null)
		{
			line = reader.ReadLine();

			if (line != null)
			{
				string[] entries = line.Split('\t');
				if (entries.Length > 0)
				{
					GameObject card = Instantiate (cardTemplate, cardPosition, Quaternion.identity) as GameObject;
					card.GetComponent<Card>().create(entries[0], entries[8], int.Parse(entries[3]), int.Parse(entries[1]), int.Parse(entries[4]), int.Parse(entries[5]), int.Parse(entries[6]), int.Parse(entries[7]));
					cardDatabase.Add(card);
					cardPosition += Vector3.right * 5;
				}
			}
		}

		reader.Close();
	}
}
