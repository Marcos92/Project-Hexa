using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour 
{
	private List<Card> hand; 
	private List<Card> deck; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//HAND

	void BuildHand()
    {
        foreach(Card c in GetComponentsInChildren<Card>()) hand.Add(c);

        ArrangeHand();
	}

	void ArrangeHand()
    {
        int count = hand.Count;
		float cardSpacingWidth = 6f/count;
		/*float cardSpacingHeight = 0.2f;
        float cardSpacingDepth = 0.01f;
        float cardSpacingAngle = 0.05f;*/
        float cardPositionX;
        float cardPositionY = 0;
        float cardRotation;
		float cardDepth = 0;
		int order = 10;

        if (count % 2 != 0)
        {
            cardPositionX = -(count - 1) * cardSpacingWidth / 2f;
            /*cardPositionY = -(count - 1) * cardSpacingHeight / 2f;
            cardRotation = (count / 2) * cardSpacingAngle;*/
        }
        else
        {
            cardPositionX = -(count / 2f - 0.5f) * cardSpacingWidth;
            /*cardPositionY = -(count / 2f - 0.5f) * cardSpacingHeight;
            cardRotation = (count - 1) * 0.5f * cardSpacingAngle;*/
        }

        foreach(Card c in hand)
        {
			c.transform.localPosition = new Vector3(cardPositionX, cardPositionY, cardDepth);

            /*Quaternion r = c.transform.localRotation;
            c.transform.localRotation = new Quaternion(r.x, r.y, cardRotation, r.w);*/

			c.originalDepth = cardDepth;

            c.originalHeight = c.transform.localPosition.y;

			c.originalOrder = order;
			c.transform.GetComponent<SpriteRenderer> ().sortingOrder = order;
			c.transform.Find ("Canvas").gameObject.GetComponent <Canvas>().sortingOrder = order++;

            cardPositionX += cardSpacingWidth;

            /*if(count % 2 == 0 && cards.IndexOf(c) == count / 2) //If it's the card to the right of the middle point (even hand) start decreasing height
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

			cardDepth -= cardSpacingDepth;*/
        }
    }

	void PrintHand()
	{
		foreach(Card c in hand)
		{
			c.Print();
		}
	}

	//DECK

	public void BuildDeck()
	{
		for(int i = 0; i < 30; i++)
		{
			deck.Add(Database.cardDatabase[i]);
		}

		PrintDeck();
	}

	public int CountDeck()
	{
		return deck.Count;
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

	void PrintDeck()
	{
		foreach(Card c in deck)
		{
			c.Print();
		}
	}
}
