using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMoveObject : MonoBehaviour
{
    //画面外に出た時の移動先
    Vector2 warpPos;

    //移動方向
    Vector2 moveVec;

    //スピード
    float moveSpeed = 1f;
    float rotateSpeed = 1f;

    // Start is called before the first frame update
    public void MoveStart(Vector2 _warpPos, Vector2 _moveVec, float _moveSpeed, float _rotateSpeed)
    {
        warpPos = _warpPos;
        moveVec = _moveVec;
        moveSpeed = _moveSpeed;
        rotateSpeed = _rotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += (Vector3)(moveVec * moveSpeed * Time.deltaTime);

        if(transform.position.x <= -12 || transform.position.y <= -6)
        {
            transform.position = warpPos;
        }

        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
