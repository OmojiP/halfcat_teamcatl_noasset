using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessEffect : MonoBehaviour
{
    // Bless�̂����ꏊ�ɐ�������A��莞�Ԃŏ�����

    private void Start()
    {
        PlayEffect().Forget();
    }


    private async UniTask PlayEffect()
    {
        await UniTask.Delay(1000);

        Destroy(gameObject);
    }
}
