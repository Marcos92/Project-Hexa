using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    //public int cost, attack, currentHealth, maxHealth, speed, range;
    public Card card;
    public int moves = 1, attacks = 1;
    public bool ally;

	public Creature(Card c, bool a)
    {
        card = c;
        ally = a;
        //card.Print();
    }
}
