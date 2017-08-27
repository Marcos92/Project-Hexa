using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{
    public Creature creature;

    public float width, height;
    [HideInInspector]
    public int x, y, z;
    [HideInInspector]
    public bool selected = false;

    [Header("Colors")]
    public Color normalColor;
    public Color highlightColor;
    public Color selectedColor;
    public Color rangeColor;
    public Color allyColor;
    public Color enemyColor;
    public Color canAttackColor;

    [HideInInspector]
    public Color color;
    //[HideInInspector]
    public Color oldColor;

    //Events
    public delegate void EnterCell(Cell c); 
    public static event EnterCell OnEnterCell;
    public delegate void ExitCell(Cell c); 
    public static event ExitCell OnExitCell;
    public delegate void ClickCell(Cell c); 
    public static event ClickCell OnClickCell;

    void Start ()
    {
        ChangeColor(normalColor);
    }
	
	void Update ()
    {
	
	}

    public bool Empty()
    {
        return creature == null;
    }

    //Setup

    public void AssignGrid(GameObject g)
    {
        transform.SetParent(g.transform);
    }

    public void GetCubicCoordinates(int row, int col)
    {
        x = col - (row + (row & 1)) / 2;
        z = row;
        y = -x - z;
    }

    //Colors

    public void ChangeColor(Color color)
    {
        this.color = color;
        GetComponent<SpriteRenderer>().color = this.color;
    }

    //Mouse events

    void OnMouseEnter()
    {
        OnEnterCell(this);
    }

    void OnMouseExit()
    {
        OnExitCell(this);
    }

    void OnMouseDown()
    {
        OnClickCell(this);
    }

    void OnMouseOver()
    {
        ChangeColor(highlightColor);
        //Show card details and stuff
    }
}
