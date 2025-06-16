using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int index;

    public bool IsForwardWall = true;
    public bool IsBackWall = true;
    public bool IsLeftWall = true;
    public bool IsRightWall = true;
    public bool Isfloor = true;
    public bool IsfTraps = false;

    public int randomNum;

    public GameObject forwardWall;
    public GameObject backWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject floor;
    public GameObject Trap;

    private void Start()
    {
        ShowWalls();
    }
    public void ShowWalls()
    {
        randomNum = Random.Range(0, 100);

        forwardWall.SetActive(IsForwardWall);
        backWall.SetActive(IsBackWall);
        leftWall.SetActive(IsLeftWall);
        rightWall.SetActive(IsRightWall);
        floor.SetActive(Isfloor);


        if (randomNum <= 2)
        {
            floor.SetActive(false);
            //Trap.SetActive(true);

        }
        if (forwardWall && randomNum <= 1) Trap.SetActive(true);
        if (backWall && randomNum <= 1) Trap.SetActive(true);
        if (leftWall && randomNum <= 1) Trap.SetActive(true);
        if (rightWall && randomNum <= 1) Trap.SetActive(true);

    }
    public bool CheckAllWall()
    {
        return IsForwardWall && IsBackWall && IsLeftWall && IsRightWall;
    }
}
