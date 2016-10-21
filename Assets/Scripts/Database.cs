using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class Database : MonoBehaviour
{
	static public List<Card> cardDatabase; 

	public void buildCardDatabase()
	{
		cardDatabase = new List<Card> ();

		string line;
		StreamReader reader = new StreamReader ("Assets/creaturesDatabase.txt");

		line = reader.ReadLine(); //Discard first line

		do
		{
			line = reader.ReadLine();

			if (line != null)
			{
				string[] entries = line.Split('\t');
				if (entries.Length > 0)
				{
					cardDatabase.Add(new Card(entries[0], entries[8], int.Parse(entries[3]), int.Parse(entries[1]), int.Parse(entries[5]), int.Parse(entries[4]), int.Parse(entries[6]), int.Parse(entries[7])));
					cardDatabase[cardDatabase.Count - 1].print();
				}
			}
		}
		while (line != null);

		reader.Close();
	}
}
