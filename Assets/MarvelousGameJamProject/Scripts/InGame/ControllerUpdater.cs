using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUpdater : MonoBehaviour
{
    // Updateの順序を管理
    [SerializeField] ArmController[] arms;
    [SerializeField] HandController[] hands;
    [SerializeField] DishController dish;

    public bool isExistStew;


    // Update is called once per frame
    void Update()
    {

        if (isExistStew)
        {
            // ハンドの位置をキー入力で更新
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].HandUpdate();
            }
        }

        // dishの位置をハンド位置の重心に移動し、さらにハンド位置を修正
        dish.DishUpdate();

        // 腕の位置を更新、腕に合わせてハンドを回転
        for (int i = 0; i < arms.Length; i++)
        {
            arms[i].ArmUpdate();
        }

    }
}