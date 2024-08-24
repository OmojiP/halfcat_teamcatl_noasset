using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGroundManager : MonoBehaviour
{
    [SerializeField] TitleMoveObject[] titleMoveObjects;
    [SerializeField] Transform moveObjParent;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    Vector2 moveVec = new Vector2(-1, -1);

    void Start()
    {
        for (int x = -12; x < 12; x += 4)
        {
            for (int y = -6; y < 6; y += 4)
            {
                int r = Random.Range(0, titleMoveObjects.Length);
                var m = Instantiate(titleMoveObjects[r], new Vector2(x, y), Quaternion.Euler(0, 0, Random.Range(0, 360)), moveObjParent);
                m.MoveStart(GetWarpPos(x, y), moveVec, moveSpeed, rotateSpeed);
            }
        }
    }

    Vector2 GetWarpPos(int x , int y)
    {
        while (true)
        {
            if (x >= 12 || y >= 6)
            {
                return new Vector2(x, y);
            }
            //else if (x >= 12)
            //{
            //    return new Vector2(x, y);
            //}
            //else if (y >= 6)
            //{
            //    return new Vector2(x, y);
            //}


            x++;
            y++;
        }
    }
}
