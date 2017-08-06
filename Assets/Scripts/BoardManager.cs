using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    List<Cell> cells;
    [HideInInspector]
    public Cell cell;
    public int sizeX = 10, sizeZ = 10;
    public Creature allyCreature, evilCreature; //DELETE LATER
    public GameObject grid;

	void Start ()
    {
        cells = new List<Cell>();

        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Cell c = Instantiate(cell, transform.position, Quaternion.Euler(90, 90, 0)) as Cell;

                if (z % 2 == 0) c.transform.position += new Vector3(x * cell.width, 0, -z * cell.height * 0.88f);
                else c.transform.position += new Vector3(x * cell.width - 0.5f * cell.width, 0, -z * cell.height * 0.88f);

                c.AssignGrid(grid);
                c.GetCubicCoordinates(z, x);

                cells.Add(c);
            }
        }

        //Events
        Cell.OnEnterCell += HandleEnterCell;
        Cell.OnExitCell += HandleExitCell;
        Cell.OnClickCell += HandleClickCell;
	}

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Summon(cells[0], allyCreature);
            Summon(cells[1], evilCreature);
            Summon(cells[20], evilCreature);
            Summon(cells[40], evilCreature);
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectCell();
        }
	}

    public List<Cell> GetCellsInRange(Cell c)
    {
        List<Cell> list = new List<Cell>();
        if(c.creature == null) return list;

        int range = c.creature.range;

        int maxX = c.x + range;
        int minX = c.x - range;
        int maxY = c.y + range;
        int minY = c.y - range;
        int maxZ = c.z + range;
        int minZ = c.z - range;

        foreach(Cell cell in cells)
        {
            if (cell.x >= minX && cell.x <= maxX && cell.y >= minY && cell.y <= maxY && cell.z >= minZ && cell.z <= maxZ
                && (cell.x != c.x || cell.y != c.y || cell.z != c.z) && cell.creature == null)
            {
                list.Add(cell);
            } 
        }

        return list;
    }

    public List<Cell> GetEnemiesInRange(Cell c)
    {
        List<Cell> list = new List<Cell>();
        if(c.creature == null) return list;

        int range = c.creature.range;

        int maxX = c.x + range;
        int minX = c.x - range;
        int maxY = c.y + range;
        int minY = c.y - range;
        int maxZ = c.z + range;
        int minZ = c.z - range;

        foreach (Cell cell in cells)
        {
            if (cell.x >= minX && cell.x <= maxX && cell.y >= minY && cell.y <= maxY && cell.z >= minZ && cell.z <= maxZ
                && (cell.x != c.x || cell.y != c.y || cell.z != c.z) && cell.creature != null && !cell.creature.ally)
            {
                list.Add(cell);
            }
        }

        return list;
    }

    public List<Cell> GetAllEnemies()
    {
        List<Cell> list = new List<Cell>();

        foreach (Cell cell in cells)
        {
            if (cell.creature != null && !cell.creature.ally)
            {
                list.Add(cell);
            }
        }

        return list;
    }

    public Cell GetSelectedCell()
    {
        foreach (Cell cell in cells)
        {
            if (cell.selected) return cell;
        }

        return null;
    }

    void SelectCell(Cell c)
    {
        /*if (GetSelectedCell() != null)
        {
            GetSelectedCell().Deselect();
        }

        if(!creature.moved)
        {
            GetCellsInRange(this, creature.speed);
            ChangeColor(rangeColor, grid.cellsInRange);
        }

        if(!creature.attacked)
        {
            grid.GetEnemiesInRange(this, creature.range);
            ChangeColor(canAttackColor, grid.enemiesInRange);
        }

        ChangeColor(selectedColor);*/
        c.selected = true;
    }

    void DeselectCell()
    {
        //ChangeColor(normalColor, grid.cellsInRange);
        /*grid.cellsInRange.Clear();
        ChangeColor(normalColor);*/
        if(GetSelectedCell() != null)
        {
            GetSelectedCell().selected = false;
        }
    }

    public void Summon (Cell cell, Creature newCreature)
    {
        Creature c = Instantiate(newCreature, transform.position, transform.rotation) as Creature;
        cell.creature = c;

        cell.ChangeColor(cell.creature.ally ? cell.allyColor : cell.enemyColor);
    }

    public void Move(Cell destination)
    {
        //Move creature
        Creature temp = GetSelectedCell().creature;
        destination.creature = temp;
        GetSelectedCell().creature = null;
        DeselectCell();
        destination.creature.moved = true;

        //Update target list
        if (!destination.creature.attacked)
        {
            SelectCell(destination);
        }
    }

    public void Attack(Cell target)
    {
        //Attack calculations

        GetSelectedCell().creature.attacked = true;
        if (GetSelectedCell().creature.moved) DeselectCell();
    }

    //Events

    void HandleEnterCell(Cell c)
    {
        c.oldColor = c.color;
        c.ChangeColor(c.highlightColor);
    }

    void HandleExitCell(Cell c)
    {
        c.ChangeColor(c.oldColor);
    }

    void HandleClickCell(Cell c)
    {
        if(GetSelectedCell() == null)
        {
            SelectCell(c);
        }
        else
        {
            if(c.creature == null)
            {
                Move(c);
            }
            else
            {
                if(c.creature.ally)
                {
                    SelectCell(c);
                }
                else
                {
                    Attack(c);
                }
            }
        }
    }
}
