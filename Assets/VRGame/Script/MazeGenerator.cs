using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 5;
    public int height = 5;

    public Cell cellPrefab;

    private Cell[,] cellMap;
    private List<Cell> cellHistoryList;


    
    void Start()
    {
        BatchCells();
        MakeMaze(cellMap[0, 0]);
        cellMap[0, 0].IsLeftWall = false;
        cellMap[width - 1, height - 1].IsRightWall = false;
        cellMap[0, 0].ShowWalls();
        cellMap[width - 1, height - 1].ShowWalls();

    }

    private void BatchCells()
    {
        cellMap = new Cell[width, height];
        cellHistoryList = new List<Cell>();
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
               Cell _cell = Instantiate<Cell>(cellPrefab, this.transform);
                _cell.index = new Vector2Int(x, y);
                _cell.name = "cell" + x + "_" + y;
                _cell.transform.localPosition = new Vector3(x * 4, 0, y * 4);

                cellMap[x, y] = _cell;
            }
        }
    }
    private void MakeMaze(Cell startCell)
    {
        Cell[] neighbors = GetNeighborCells(startCell);
        if(neighbors.Length > 0)
        {
            Cell nextCell = neighbors[Random.Range(0, neighbors.Length)];
            ConnectCells(startCell, nextCell);
            cellHistoryList.Add(nextCell);
            MakeMaze(nextCell);
        }
        else
        {
            if (cellHistoryList.Count > 0)
            {
                Cell LastCell = cellHistoryList[cellHistoryList.Count - 1];
                cellHistoryList.Remove(LastCell);
                MakeMaze(LastCell);
            }
        }
    }
    private Cell[] GetNeighborCells(Cell cell)
    {
        List<Cell> retCellList = new List<Cell>();
        Vector2Int index = cell.index;
        //forward
        if(index.y + 1 < height)
        {
            Cell neighbor = cellMap[index.x, index.y + 1];
            if (neighbor.CheckAllWall())
                retCellList.Add(neighbor);
        }
        //back
        if (index.y - 1 >=0)
        {
            Cell neighbor = cellMap[index.x, index.y - 1];
            if (neighbor.CheckAllWall())
                retCellList.Add(neighbor);
        }
        //left
        if (index.x - 1 >=0)
        {
            Cell neighbor = cellMap[index.x - 1, index.y];
            if (neighbor.CheckAllWall())
                retCellList.Add(neighbor);
        }
        //right
        if (index.x + 1 < width)
        {
            Cell neighbor = cellMap[index.x + 1, index.y];
            if (neighbor.CheckAllWall())
                retCellList.Add(neighbor);
        }
        return retCellList.ToArray();
    }
    private void ConnectCells(Cell c0, Cell c1)
    {
        Vector2Int dir = c0.index - c1.index;
        //forward
        if(dir.y <= -1)
        {
            c0.IsForwardWall = false;
            c1.IsBackWall = false;
        }
        //back
        else if (dir.y >= 1)
        {
            c0.IsBackWall = false;
            c1.IsForwardWall = false;
        }
        //lefy
        else if (dir.x >= 1)
        {
            c0.IsLeftWall = false;
            c1.IsRightWall = false;
        }
        //right
        else if (dir.x <= -1)
        {
            c0.IsRightWall = false;
            c1.IsLeftWall = false;
        }

        c0.ShowWalls();
        c1.ShowWalls();
    }
}
