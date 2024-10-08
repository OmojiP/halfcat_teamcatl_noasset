using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessEffect : MonoBehaviour
{
    // Blessのいた場所に生成され、一定時間で消える

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
