using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{
    Grid grid;
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

    void Start ()
    {
        ChangeColor(normalColor);
    }
	
	void Update ()
    {
	
	}

    //Setup

    public void AssignGrid(Grid g)
    {
        transform.SetParent(g.transform);
        grid = g;
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

    public void ChangeColor(Color color, List<Cell> cells)
    {
        foreach(Cell c in cells)
        {
            c.GetComponent<SpriteRenderer>().color = color;
        }
    }

    //Cell operations

    void Select()
    {
        if (grid.GetSelectedCell() != null)
        {
            grid.GetSelectedCell().Deselect();
        }

        if(!creature.moved)
        {
            grid.GetCellsInRange(this, creature.speed);
            ChangeColor(rangeColor, grid.cellsInRange);
        }

        if(!creature.attacked)
        {
            grid.GetEnemiesInRange(this, creature.range);
            ChangeColor(canAttackColor, grid.enemiesInRange);
        }

        ChangeColor(selectedColor);
        selected = true;
    }

    void Deselect()
    {
        ChangeColor(normalColor, grid.cellsInRange);
        grid.cellsInRange.Clear();
        ChangeColor(normalColor);
        selected = false;
    }

    //Creature operations

    public void Summon (Creature newCreature)
    {
        Creature c = Instantiate(newCreature, transform.position, transform.rotation) as Creature;
        creature = c;

        ChangeColor(allyColor);
    }

    public void MoveCreature ()
    {
        //Move creature
        Creature temp = grid.GetSelectedCell().creature;
        creature = temp;
        grid.GetSelectedCell().creature = null;
        grid.GetSelectedCell().Deselect();
        creature.moved = true;

        //Update target list
        if (!creature.attacked)
        {
            Select();
            ChangeColor(enemyColor, grid.GetAllEnemies());
            grid.GetEnemiesInRange(this, creature.range);
            ChangeColor(canAttackColor, grid.enemiesInRange);
        }
    }

    public void Attack ()
    {
        //Attack calculations

        grid.GetSelectedCell().creature.attacked = true;
        if (grid.GetSelectedCell().creature.moved) Deselect();

        //Clear target list
        grid.enemiesInRange.Clear();
        ChangeColor(enemyColor, grid.GetAllEnemies());
    }

    //Mouse operations

    void OnMouseOver()
    {
        ChangeColor(highlightColor);
    }

    void OnMouseExit()
    {
        if (selected) ChangeColor(selectedColor);
        else if (creature != null && creature.ally) ChangeColor(allyColor);
        else if (creature != null && grid.enemiesInRange.Contains(this)) ChangeColor(canAttackColor);
        else if (creature != null) ChangeColor(enemyColor);
        else if (grid.cellsInRange.Contains(this)) ChangeColor(rangeColor);
        else ChangeColor(normalColor);
    }

    void OnMouseDown()
    {
        if (selected) Deselect(); //Selected cell
        else if (!grid.cellsInRange.Contains(this) && creature != null && creature.ally) Select(); //Ally creature cell
        else if (grid.enemiesInRange.Contains(this)) Attack(); //Enemy creature cell
        else if (grid.cellsInRange.Contains(this)) MoveCreature(); //Cell in creature move range
    }
}
