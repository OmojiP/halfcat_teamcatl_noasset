using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndCats : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    [SerializeField] float moveTime;


    public void MoveStart()
    {
        transform.position = startPos;
        transform.DOMove(endPos, moveTime).OnComplete(() => Destroy(this.gameObject));
    }
}
