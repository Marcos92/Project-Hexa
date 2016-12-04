﻿using UnityEngine;
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
		float cardSpacingWidth = 6f/count;
		float cardSpacingHeight = 0.1f;
        float cardSpacingDepth = 0.01f;
        float cardSpacingAngle = 0.05f;
        float cardPositionX;
        float cardPositionY;
        float cardRotation;
		float cardDepth = 0;
		int order = 10;

        if (count % 2 != 0)
        {
            cardPositionX = -(count - 1) * cardSpacingWidth / 2f;
            cardPositionY = -(count - 1) * cardSpacingHeight / 2f;
            cardRotation = (count / 2) * cardSpacingAngle;
        }
        else
        {
            cardPositionX = -(count / 2f - 0.5f) * cardSpacingWidth;
            cardPositionY = -(count / 2f - 0.5f) * cardSpacingHeight;
            cardRotation = (count - 1) * 0.5f * cardSpacingAngle;
        }

        foreach(Card c in cards)
        {
			c.transform.localPosition = new Vector3(cardPositionX, cardPositionY, cardDepth);

            Quaternion r = c.transform.localRotation;
            c.transform.localRotation = new Quaternion(r.x, r.y, cardRotation, r.w);

			c.originalDepth = cardDepth;

			c.originalOrder = order;
			c.transform.GetComponent<SpriteRenderer> ().sortingOrder = order;
			c.transform.FindChild ("Canvas").gameObject.GetComponent <Canvas>().sortingOrder = order++;

            cardPositionX += cardSpacingWidth;

            if(count % 2 == 0 && cards.IndexOf(c) == count / 2) //If it's the card to the right of the middle point (even hand) start decreasing height
            {
                cardSpacingHeight *= -1;
            }

            if (count % 2 != 0 && cards.IndexOf(c) == count / 2) //If it's the middle card (odd hand) start decreasing height
            {
                cardSpacingHeight *= -1;
            }

            if (!(count % 2 == 0 && cards.IndexOf(c) == count / 2 - 1)) //If it's the card to the left of the middle point (even hand) don't add height to next card
            {
                cardPositionY += cardSpacingHeight;
            }

            cardRotation -= cardSpacingAngle;

			cardDepth -= cardSpacingDepth;
        }
    }
}
