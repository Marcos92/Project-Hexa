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
        int count = cards.Count;
		float cardSpacingWidth = 5f/count;
		float cardSpacingDepth = 0.01f;
        float cardSpacingAngle = 0.1f;
		float cardPosition;
        float cardRotation;
		float cardDepth = 0;
		int order = 10;

        if (count % 2 != 0)
        {
            cardPosition = -(count - 1) * cardSpacingWidth / 2f;
            cardRotation = (count / 2) * cardSpacingAngle;
        }
        else
        {
            cardPosition = -(count / 2f - 0.5f) * cardSpacingWidth;
            cardRotation = (count - 1) * 0.5f * cardSpacingAngle;
        }

        foreach(Card c in cards)
        {
            Vector3 p = c.transform.localPosition;
			c.transform.localPosition = new Vector3(cardPosition, p.y, cardDepth);

            Quaternion r = c.transform.localRotation;
            c.transform.localRotation = new Quaternion(r.x, r.y, cardRotation, r.w);

			c.originalDepth = cardDepth;

			c.originalOrder = order;
			c.transform.GetComponent<SpriteRenderer> ().sortingOrder = order;
			c.transform.FindChild ("Canvas").gameObject.GetComponent <Canvas>().sortingOrder = order++;

			cardPosition += cardSpacingWidth;
            cardRotation -= cardSpacingAngle;
			cardDepth -= cardSpacingDepth;
        }
    }
}
