using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUpdater : MonoBehaviour
{
    // Update�̏������Ǘ�
    [SerializeField] ArmController[] arms;
    [SerializeField] HandController[] hands;
    [SerializeField] DishController dish;

    public bool isExistStew;


    // Update is called once per frame
    void Update()
    {

        if (isExistStew)
        {
            // �n���h�̈ʒu���L�[���͂ōX�V
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].HandUpdate();
            }
        }

        // dish�̈ʒu���n���h�ʒu�̏d�S�Ɉړ����A����Ƀn���h�ʒu���C��
        dish.DishUpdate();

        // �r�̈ʒu���X�V�A�r�ɍ��킹�ăn���h����]
        for (int i = 0; i < arms.Length; i++)
        {
            arms[i].ArmUpdate();
        }

    }
}