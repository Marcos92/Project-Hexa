using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    List<Cell> cells;
    [HideInInspector]
    public List<Cell> cellsInRange, enemiesInRange;
    public Cell cell;
    public int sizeX = 10, sizeZ = 10;
    public Creature creature, evilCreature; //DELETE LATER

	void Start ()
    {
        cells = new List<Cell>();
        cellsInRange = new List<Cell>();
        enemiesInRange = new List<Cell>();

        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Cell c = Instantiate(cell, transform.position, Quaternion.Euler(90, 90, 0)) as Cell;

                if (z % 2 == 0) c.transform.position += new Vector3(x * cell.width, 0, -z * cell.height * 0.88f);
                else c.transform.position += new Vector3(x * cell.width - 0.5f * cell.width, 0, -z * cell.height * 0.88f);

                c.AssignGrid(this);
                c.GetCubicCoordinates(z, x);

                cells.Add(c);
            }
        }
	}

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cells[0].Summon(creature);
            cells[1].Summon(evilCreature);
            cells[20].Summon(evilCreature);
            cells[40].Summon(evilCreature);
        }
	}

    public void GetCellsInRange(Cell c, int range)
    {
        cellsInRange.Clear();

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
                cellsInRange.Add(cell);
            } 
        }
    }

    public void GetEnemiesInRange(Cell c, int range)
    {
        enemiesInRange.Clear();

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
                enemiesInRange.Add(cell);
            }
        }
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
}
