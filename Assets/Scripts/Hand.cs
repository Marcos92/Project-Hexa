using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    List<Card> cards;

	void Start ()
    {
        cards = new List<Card>();

        foreach(Card c in GetComponentsInChildren<Card>()) cards.Add(c);

        ArrangeCards();
	}
	
	void Update ()
    {
	
	}

    void ArrangeCards()
    {
		float cardSpacingWidth = 1.5f;
		float cardSpacingDepth = 0.05f;
        int count = cards.Count;
		float cardPosition;
		float cardDepth = 0;
		int order = 10;

		if (count % 2 != 0) cardPosition = -(count - 1) * cardSpacingWidth / 2f;
		else cardPosition = -(count / 2f - 0.5f) * cardSpacingWidth;

        foreach(Card c in cards)
        {
            Vector3 p = c.transform.localPosition;
			c.transform.localPosition = new Vector3(cardPosition, p.y, cardDepth);
			c.transform.GetComponent<SpriteRenderer> ().sortingOrder = order++;
			cardPosition += cardSpacingWidth;
			cardDepth -= cardSpacingDepth;
        }
    }
}
