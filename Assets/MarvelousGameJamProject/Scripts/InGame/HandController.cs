using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;
using MarvelousGameJamProject;

public class HandController : MonoBehaviour
{
    [SerializeField] private float speed;
    public HandType handType;
    public ControlMode _controlMode;

    [SerializeField] Vector2 handCenterPos;
    [SerializeField] Vector2 handOutOfScreenPos;

    [SerializeField] Vector2 screenRightTopPos;
    [SerializeField] Vector2 screenLeftBottomPos;

    // 操作可能なときだけ呼ばれる
    public void HandUpdate()
    {
        // ControlSchemeに基づいて操作方法を決定
        if (!StaticSettings.IsInvertControl) //正常
        {
            if (handType == HandType.Right)
            {
                HandleArrowControls();
            }
            else if (handType == HandType.Left)
            {
                HandleWASDControls();
            }
        }
        else //反転
        {
            if (handType == HandType.Right)
            {
                HandleWASDInvertControls();
            }
            else if (handType == HandType.Left)
            {
                HandleArrowInvertControls();
            }
        }


        // 画面外に出たら押し戻す
        if (transform.position.x >= screenRightTopPos.x)
        {
            //Debug.Log("画面外に出たので押し戻す");
            transform.position = new Vector2(screenRightTopPos.x, transform.position.y);
        }
        else if (transform.position.x <= screenLeftBottomPos.x)
        {
            //Debug.Log("画面外に出たので押し戻す");
            transform.position = new Vector2(screenLeftBottomPos.x, transform.position.y);
        }
        if (transform.position.y >= screenRightTopPos.y)
        {
            //Debug.Log("画面外に出たので押し戻す");
            transform.position = new Vector2(transform.position.x, screenRightTopPos.y);
        }
        else if (transform.position.y <= screenLeftBottomPos.y)
        {
            //Debug.Log("画面外に出たので押し戻す");
            transform.position = new Vector2(transform.position.x, screenLeftBottomPos.y);
        }
    }


    // 通常モードの操作
    void HandleWASDControls()
    {
        // WASDキーでの操作
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
    void HandleArrowControls()
    {
        // 矢印キーでの操作
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    // 反転モードの操作(上下左右反転)
    void HandleWASDInvertControls()
    {
        // WASDキーでの操作
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    void HandleArrowInvertControls()
    {
        // 矢印キーでの操作
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }


    // 演出用
    // Handを画面下に瞬間移動する
    public void SetPosOutOfScreen()
    {
        transform.position=handOutOfScreenPos;
    }
    // Handを画面下に移動する
    public async UniTask MoveHandOutOfScreen()
    {
        await transform.DOMove(handOutOfScreenPos, 1f).ToUniTask();
    }
    // Handを画面中央に移動する
    public async UniTask MoveHandCenterOfScreen()
    {
        await transform.DOMove(handCenterPos, 1f).ToUniTask();
    }


    // ControlSchemeを外部から設定するメソッド
    public void SetControlScheme(ControlMode mode)
    {
        _controlMode = mode;
    }

    // 現在のControlSchemeを取得するメソッド
    public ControlMode GetControlScheme()
    {
        return _controlMode;
    }
}

public enum HandType
{
    Right,
    Left
}

public enum ControlMode
{
    Parallel,
    Cross
}