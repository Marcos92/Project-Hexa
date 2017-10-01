using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour 
{
	private List<Card> deck;

	public Transform hand;
	public Transform deckTransform;

	public GameObject cardPrefab;

	public Transform selectedCard; 

	// Use this for initialization
	void Start () 
	{
		BuildDeck();
		BuildHand();

		Card.OnClickCard += HandleClickCard;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void InstantiateCard(Card card, Vector3 position, Quaternion rotation)
	{
		GameObject newCard = Instantiate(cardPrefab, position, rotation);
		newCard.transform.parent = hand;
		newCard.GetComponent<Card>().Create(card.GetCurrentInfo());
		newCard.GetComponent<Card>().CreateVisual();
	}

	//HAND

	void BuildHand()
    {
		for(int i = 0; i < 5; i++) 
		{
			Draw(deck[i]);
		}

        ArrangeHand();
	}

	void Draw(Card card)
	{
		InstantiateCard(card, hand.position, hand.rotation);
	}

	void ArrangeHand()
    {
		int count = hand.childCount;
		float cardSpacingWidth = 6f/count;
		//float cardSpacingHeight = 0.2f;
        float cardSpacingDepth = 0.01f;
        //float cardSpacingAngle = 0.05f;
        float cardPositionX;
        float cardPositionY = 0;
        //float cardRotation;
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

		Transform[] children = new Transform[hand.childCount];

		for(int i = 0; i < hand.childCount; i++)
		{
			children[i] = hand.GetChild(i);
		}

        foreach(Transform t in children)
        {
			GameObject obj = t.gameObject;
			Card card = obj.GetComponent<Card>();

			obj.transform.localPosition = new Vector3(cardPositionX, cardPositionY, cardDepth);

            /*Quaternion r = c.transform.localRotation;
            c.transform.localRotation = new Quaternion(r.x, r.y, cardRotation, r.w);*/

			card.originalDepth = cardDepth;

            card.originalHeight = obj.transform.localPosition.y;

			card.originalOrder = order;
			obj.transform.GetComponent<SpriteRenderer> ().sortingOrder = order;
			obj.transform.Find ("Canvas").gameObject.GetComponent <Canvas>().sortingOrder = order++;

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

            cardRotation -= cardSpacingAngle;*/

			cardDepth -= cardSpacingDepth;
        }
    }

	public void Play()
	{
		for(int i = 0; i < hand.childCount; i++)
		{
			if(hand.GetChild(i) == selectedCard)
			{
				DestroyImmediate(hand.GetChild(i).gameObject);
				selectedCard = null;
				break;
			}
		}

		ArrangeHand();
	}

	void PrintHand()
	{
		for(int i = 0; i < hand.childCount; i++)
		{
			hand.GetChild(i).GetComponent<Card>().Print();
		}
	}

	//DECK

	public void BuildDeck()
	{
		deck = new List<Card>();
		
		for(int i = 0; i < 30; i++)
		{
			/* GameObject c = Instantiate(cardPrefab, deckTransform.position, deckTransform.rotation);
			c.GetComponent<Card>().CreateCreatureFromInfo(Database.cardDatabase[i]); */
			Card c = new Card(Database.cardDatabase[i]);
			deck.Add(c);
		}

		//PrintDeck();
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

	// EVENTS

	void HandleClickCard(Card c)
	{
		selectedCard = c.gameObject.transform;
		//selectedCard.Print();
	}
}
