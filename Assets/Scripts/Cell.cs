﻿using UnityEngine;
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
        GetComponent<SpriteRenderer>().color = color;
    }

    //Mouse events

    void OnMouseOver()
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
}
